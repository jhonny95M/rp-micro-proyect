using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RealPlaza.Web.Web.Middleware.ExceptionHandling;
using System.Diagnostics.CodeAnalysis;
using test01.Api.Endpoints.v1.Roles;
using test01.Api.Endpoints.v1.Users;

namespace test01.Api
{
    [ExcludeFromCodeCoverage]
    public static class EndpointRoutes
    {
        public static void ConfigureEndpoints(this WebApplication app)
        {
            app.MapPost("/v1/users", CreateUserEndpoint.DoAsync)
                .CacheOutput(b => b.NoCache())
                .Produces<HttpStatusCodeInfo>(500)
                .WithTags("Users");
            app.MapGet("/v1/users", GetUsersEndpoint.DoAsync)
                .CacheOutput(b => b.NoCache())
                .Produces<HttpStatusCodeInfo>(500)
                .WithTags("Users");

            app.MapGet("/v1/users/{id}", GetUserByIdEndpoint.DoAsync)
                .CacheOutput(b => b.NoCache())
                .Produces<HttpStatusCodeInfo>(500)
                .WithTags("Users");
            app.MapPut("/v1/users/{id}", UpdateUserEndpoint.DoAsync)
                .CacheOutput(b => b.NoCache())
                .Produces<HttpStatusCodeInfo>(500)
                .WithTags("Users");
            app.MapPut("/v1/users/status/{id}", UpdateUserStatusEndpoint.DoAsync)
                .CacheOutput(b => b.NoCache())
                .Produces<HttpStatusCodeInfo>(500)
                .WithTags("Users");
            app.MapDelete("/v1/users/{id}", DeleteUserEndpoint.DoAsync)
                .CacheOutput(b => b.NoCache())
                .Produces<HttpStatusCodeInfo>(500)
                .WithTags("Users");

            app.MapGet("/v1/roles", GetRolEndpoint.DoAsync)
                .CacheOutput(b => b.NoCache())
                .Produces<HttpStatusCodeInfo>(500)
                .WithTags("Roles");

        }
    }
}
