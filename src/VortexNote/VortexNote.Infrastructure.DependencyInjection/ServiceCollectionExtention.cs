using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VortexNote.Infrastructure.Persistence;
using VortextNote.Domain.Base.Interfaces;

namespace VortexNote.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtention
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("Sqlite");
            services.AddDbContext<AppDbContext>(x=>x.UseSqlite(connString));
            services.AddTransient<IAppDbContext, AppDbContext>();
            return services;
        }
    }
}