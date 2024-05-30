using AutomationProvider.Api.Commmon.Mapping;
using AutomationProvider.Api.Policies;

namespace AutomationProvider.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddMapping();
            services.AddPolicies();
 
            return services;
        }
    }
}
