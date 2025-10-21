using TicketingHub.Api.Common.Interfaces;
using TicketingHub.Api.Infrastructure.Services;

namespace TicketingHub.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
                options.AddPolicy("CorsPolicy", policyBuilder =>
                {
                    policyBuilder.WithOrigins("http://localhost:3000/")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowed((host) => true);
                }));

            // SLA services
            services.AddScoped<ISlaCalculationService, SlaCalculationService>();
            services.AddScoped<ISlaMonitoringService, SlaMonitoringService>();

            // Notification & subscriptions
            services.AddScoped<INotificationPublisher, NotificationPublisher>();
            services.AddScoped<ITicketSubscriptionService, TicketSubscriptionService>(); // implement separately


            services.AddScoped<ITicketAssignmentService, TicketAssignmentService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddSignalR();

            return services;
        }
    }
}
