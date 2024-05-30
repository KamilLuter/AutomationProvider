using AutomationProvider.Application.Common.Interfaces.Authentication;
using AutomationProvider.Application.Common.Interfaces.Services;
using AutomationProvider.Infrastructure.Authentication;
using AutomationProvider.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Text;
using AutomationProvider.Application.Common.Interfaces.Persistance;
using AutomationProvider.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutomationProvider.Infrastructure.Persistance.Repositories;
using AutomationProvider.Infrastructure.Idempotency;
using AutomationProvider.Infrastructure.Interceptors;
using Quartz;
using AutomationProvider.Infrastructure.BackgroundJobs;
using AutomationProvider.Infrastructure.Services.Email;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Logging;

namespace AutomationProvider.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.AddAuth(configuration);
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IIdempotencyService, IdempotencyService>();
            services.AddPersistance(configuration);
            // services.AddEmail(configuration);

            services.Configure<SmtpConfiguration>(configuration.GetSection(SmtpConfiguration.SectionName));

            services.AddSingleton<IEmailService, SmtpEmailService>();


            services.AddQuartz(configure =>
            {
                var jobKey = new JobKey(nameof(ProcessOutboxMessageJob));
                configure
                    .AddJob<ProcessOutboxMessageJob>(jobKey)
                    .AddTrigger(
                        trigger =>
                            trigger.ForJob(jobKey)
                                   .WithSimpleSchedule(
                                        schedule =>
                                            schedule.WithIntervalInSeconds(20)
                                                    .RepeatForever()));
            });

            services.AddQuartzHostedService();

            services.AddLogging(builder =>
            {
                builder.AddConsole();
            });

            return services;
        }

        public static IServiceCollection AddAuth(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);

            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                });

            services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<AutomationProviderDbContext>().AddDefaultTokenProviders(); ;

            services.AddScoped<IPasswordValidator<User>, PasswordValidator<User>>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = true;
            });

            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserManager, UserManager>();

            return services;
        }

        public static IServiceCollection AddPersistance(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddSingleton<ConvertDomainEventsToOutboxMessageInterceptor>();

            services.AddDbContext<AutomationProviderDbContext>((sp, options) =>
            {
                var interceptor = sp.GetServices<ConvertDomainEventsToOutboxMessageInterceptor>();
                options.UseSqlServer(connectionString)
                .AddInterceptors(interceptor);
            });

            services.AddMemoryCache();

            services.AddScoped<ProductRepository>();
            services.AddScoped<IProductRepository, CachedProductRepository>();
            services.AddScoped<ProductsSqlBuilder>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICatalogRepository, CatalogRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();

            return services;
        }

        public static IServiceCollection AddEmail(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.Configure<SmtpConfiguration>(configuration.GetSection(SmtpConfiguration.SectionName));

            services.AddSingleton<IEmailService, SmtpEmailService>();

            return services;
        }
    }
}
