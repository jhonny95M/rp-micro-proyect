using Microsoft.Extensions.DependencyInjection;
using RealPlaza.Core.Common.Service.Implementations;
using RealPlaza.Core.Common.Service.Interfaces;
using System.Diagnostics.CodeAnalysis;
using test01.Persistence;
using test01.Persistence.Roles.Core.Queries;
using test01.Persistence.UserRequests.Core.Queries;
using test01.Persistence.UserRequests.Repository;

namespace test01.Application
{
    [ExcludeFromCodeCoverage]
    public static class InjectionsSetup
    {
        public static void ConfigureInjections(this IServiceCollection services)
        {
            // Repositories
            services.AddTransient<IUserRepository, UserRepository>();
            // UnitOfWork
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // Services
            services.AddTransient<IDateService, DateService>();

            // Queries
            services.AddScoped<IUserQueries, UserQueries>();
            services.AddScoped<IRolQueries, RolQueries>();
        }
    }
}
