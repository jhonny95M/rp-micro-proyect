using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using JasperFx.CodeGeneration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Polly;
using Polly.Extensions.Http;
using RealPlaza.ApiDocumentation.Extensions;
using RealPlaza.Core.Common.Service.Implementations;
using RealPlaza.Core.Common.Service.Interfaces;
using RealPlaza.Core.Core.Configuration;
using RealPlaza.Core.Core.Persistence;
using RealPlaza.Observability;
using RealPlaza.Observability.Middleware;
using RealPlaza.Web.Web.HealthCheck;
using RealPlaza.Web.Web.Middleware.ExceptionHandling;
using RealPlaza.Web.Web.Middleware.HeaderPropagation;
using RealPlaza.Web.Web.Middleware.Security;
using RealPlaza.Web.Web.Middleware.TokenHandling;
using Serilog;
using Serilog.Formatting.Json;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using test01.Application;
using test01.Application.Telemetry;
using test01.Database;
using test01.Service.Utility;
using Wolverine;
using Wolverine.FluentValidation;

namespace test01.Api
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                Bootstrap.LoadEnvironmentVariables();
                builder.Configuration.AddEnvironmentVariables();

                //CONFIGURACION AZURE KEY VAULT
                if (builder.Environment.IsProduction())
                {
                    var secretClient = new SecretClient(
                        new Uri($"https://{builder.Configuration["AZURE-KEY-VAULT-NAME"]}.vault.azure.net/"),
                        new ClientSecretCredential(
                            builder.Configuration["AAD-TENANT-ID"],
                            builder.Configuration["AAD-CLIENT-ID"],
                            builder.Configuration["AAD-CLIENT-SECRET"]
                        )
                    );
                    builder.Configuration.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
                }

                builder.Services.AddCors(options =>
                {
                    options.AddPolicy(name: "AllowPortalOrigins",
                        policy =>
                        {
                            var allowedOrigins = builder.Configuration["ORIGINS_CONFIGURATION"]
                                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                                .Select(origin => origin.Trim())
                                .ToArray();

                            policy.WithOrigins(allowedOrigins)
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        });
                });

                builder.Host.UseWolverine((context, options) =>
                {
                    options.RegisterSecurityTypesForWolverine();
                    options.CodeGeneration.TypeLoadMode = TypeLoadMode.Static;
                    options.CodeGeneration.ApplicationAssembly = typeof(Bootstrap).Assembly;
                    options.UseFluentValidation();
                });
                builder.Services.AddHttpContextAccessor();

                //Configuración de Telemetría
                var instances = RealPlazaObserver.SetupObservability(builder.Host, builder.Configuration, builder.Services);
                builder.Services.AddSingleton<IInstrumentation>(new Instrumentation(instances.Meter));
                
                builder.Services.AddSingleton<IConfigurationHelper, ConfigurationHelper>();

                //Configuracion de la clase base para comunicaciones http con 3eros
                builder.Services.AddHttpClient<IHttpService, HttpService>("realplaza")
                    .AddPolicyHandler(GetRetryPolicy(builder.Configuration))
                    .AddHttpMessageHandler<HttpHeaderPropagationHandler>();

                builder.Services.AddSingleton<IHttpService>(serviceProvider =>
                {
                    var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
                    var httpService = new HttpService(httpClientFactory);
                    return httpService;
                });

                //Configuración de conexion a base de datos
                string connectionString = builder.Configuration["TESTAPP-DB"]
                       ?? throw new InvalidOperationException("No connection string found for TESTAPP-DB.");

                //Configuracion del HealthCheck
                var connectionFactory = () => new NpgsqlConnection(connectionString);
                builder.Services
                    .AddHealthChecks()
                    .AddDatabaseHealthCheck(
                             connectionFactory,
                             name: "ARQUETIPO_DB_HEALTH_CHECK",
                             tags: (ProbeType.Liveness | ProbeType.Startup).ToTags());

                builder.Services.ConfigureConnectionFactory(connectionString);
                builder.Services.ConfigureUnitOfWorkDependencies(connectionString);
                builder.Services.ConfigureInjections();

                builder.Services.AddTransient<DatabaseIntegrator>();

                //Configuración de autenticación
                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                }).AddJwtBearer(options =>
                {
                    options.Authority = builder.Configuration["IDENTITY-AUTHORITY"];
                    options.Audience = builder.Configuration["IDENTITY-API-AUDIENCE"];
                    options.RequireHttpsMetadata = builder.Configuration.GetValue<bool>("IDENTITY-HTTPS-METADATA");
                });
                builder.Services.AddAuthorization();

                builder.Services.AddTransient<ClaimsMiddleware>();
                builder.Services.AddEndpointsApiExplorer();
                //builder.Services.AddSwaggerGen();

                builder.Services.AddCustomSwagger(builder.Configuration);
                builder.Services.AddCustomApiVersioning();

                var app = builder.Build();

                using (var scope = app.Services.CreateScope())
                {
                    var databaseIntegrator = scope.ServiceProvider.GetRequiredService<DatabaseIntegrator>();
                    databaseIntegrator.Run();
                }

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseCustomSwagger(app.Configuration);
                }

                app.UseHttpsRedirection();

                app.UseCors("AllowPortalOrigins");
                app.UseAuthentication();
                app.UseAuthorization();
                app.UseMiddleware<HttpRequestLogMiddleware>();
                app.UseMiddleware<ClaimsMiddleware>();
                app.UseMiddleware<SecurityHeadersMiddleware>();

                //Configuracion para el manejo de excepciones
                app.UseExceptionHandlingMiddleware(exception => exception switch
                {
                    // Add exceptions and their mapping to HTTP Code
                    BadHttpRequestException _ => HttpStatusCode.BadRequest,
                    NotImplementedException _ => HttpStatusCode.NotImplemented,
                    _ => HttpStatusCode.InternalServerError,
                });

                var healthCheckPort = builder.Configuration["HEALTHCHECK-PORT"];
                app.MapHealthChecks("/health/startup", ProbeType.Startup.GetFilter())
                    .AllowAnonymous();

                app.MapHealthChecks("/health/liveness", ProbeType.Liveness.GetFilter())
                    .AllowAnonymous();

                app.MapHealthChecks("/health/readiness", ProbeType.Readiness.GetFilter())
                    .AllowAnonymous();

                app.UsePathBase($"/test01");
                app.ConfigureEndpoints();

                app.Run();
            }
            catch (Exception ex)
            {
                using var logger = new LoggerConfiguration()
                     .WriteTo.Console(new JsonFormatter())
                     .CreateLogger();

                logger.Error("Startup Error. Exception Type: {ExceptionType}", ex?.GetType()?.ToString(), ex);
                throw;
            }
        }

        [ExcludeFromCodeCoverage]
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(IConfiguration _configuration)
        {
            var retryCount = Convert.ToInt32(_configuration["RETRY-COUNT"]);
            var sleepDurationProvider = Convert.ToInt32(_configuration["SLEEP-DURATION-PROVIDER"]);

            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode != HttpStatusCode.Accepted)
                .WaitAndRetryAsync(retryCount, retryAttempt => TimeSpan.FromSeconds(sleepDurationProvider));
        }
    }
}
