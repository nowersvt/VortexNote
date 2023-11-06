using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VortexNote.Domain.Base.Interfaces;
using VortexNote.Infrastructure.Persistence;

namespace VortexNote.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtention
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAppDbContext(configuration);
            return services;
        }
        private static void AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("Sqlite");
            services.AddDbContext<AppDbContext>(x => x.UseSqlite(connString));
            services.AddTransient<IAppDbContext, AppDbContext>();
        }
    }
}