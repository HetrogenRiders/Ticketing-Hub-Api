using MediatR;
using Microsoft.Extensions.Logging;
using TicketingHub.Api.Common.Interfaces;

namespace TicketingHub.Api.Common.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService _currentUserService;

    public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.UserId ?? string.Empty;

        // Log the request
        _logger.LogInformation(
            "Merit Web App base framework: {Name} {@UserId} {@Request}",
            requestName,
            userId,
            request);

        // Call the next delegate in the pipeline
        var response = await next();

        return response;
    }
}
