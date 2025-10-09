using FluentValidation;
using TicketingHub.Api.Common.Behaviours;
using TicketingHub.Api.Common.Interfaces;
using TicketingHub.Api.Extensions;
using TicketingHub.Api.Infrastructure.Services;
using MediatR;

namespace TicketingHub.Api;
public static class ConfigureServices
{
    public static IServiceCollection AddService(this IServiceCollection services)
    {
        services.AddCustomCors();
        services.AddValidatorsFromAssembly(typeof(ConfigureServices).Assembly);
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(ConfigureServices).Assembly);

            options.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            options.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
            options.AddOpenBehavior(typeof(LoggingBehaviour<,>));
            options.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));

        });

        services.AddTransient<IEmailService, EmailService>();
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        return services;
    }
}

