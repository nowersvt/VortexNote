using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VortexNote.Application.Common.Mappers;
using VortexNote.Application.Common.Providers;
using VortexNote.Application.Notes.Command;
using static VortexNote.Application.Common.Providers.IIdentityProvider;

namespace VortexNote.Application.DependencyInjection
{
    public static class ServiceCollectionExtention
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {

            return services
                .AddMediatr(configuration)
                .AddProviders()
                .AddMapper();
        }
        public static IServiceCollection AddMediatr(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(x =>
            {
                x.RegisterServicesFromAssemblies(typeof(CreateNoteCommandHandler).Assembly);
            });
            return services;
        }
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(x=>x.AddProfile(new ApplicationProfile()));
            return services;
        }
        public static IServiceCollection AddProviders(this IServiceCollection services)
        {
            services.AddTransient<IIdentityProvider, IdentityProvider>();
            return services;
        }
    }
}