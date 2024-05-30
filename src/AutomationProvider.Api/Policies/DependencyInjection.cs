using AutomationProvider.Api.Commmon.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace AutomationProvider.Api.Policies
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy =>
                    policy.RequireRole("Admin"));
            });

            return services;
        }
    }
}
