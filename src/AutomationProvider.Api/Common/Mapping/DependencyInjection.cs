using AutomationProvider.Contracts.Catalog;
using AutomationProvider.Domain.CatalogAggregate;
using Mapster;
using MapsterMapper;
using System.Reflection;

namespace AutomationProvider.Api.Commmon.Mapping
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            return services;
        }
    }
}
