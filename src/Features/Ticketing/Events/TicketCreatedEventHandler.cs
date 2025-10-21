using MediatR;
using Microsoft.Extensions.Localization;
using TicketingHub.Api.Common.Interfaces;
using TicketingHub.Api.Resources;

namespace TicketingHub.Api.Features.Ticketing.Events
{
    public class TicketCreatedEventHandler : INotificationHandler<TicketCreatedEvent>
    {
        private readonly ITicketSubscriptionService _subscriptionService;
        private readonly Common.Interfaces.INotificationPublisher _notificationPublisher;
        private readonly ILogger<TicketCreatedEventHandler> _logger;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public TicketCreatedEventHandler(
            ITicketSubscriptionService subscriptionService,
            Common.Interfaces.INotificationPublisher notificationPublisher,
            ILogger<TicketCreatedEventHandler> logger,
            IStringLocalizer<SharedResource> localizer)
        {
            _subscriptionService = subscriptionService;
            _notificationPublisher = notificationPublisher;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task Handle(TicketCreatedEvent notification, CancellationToken cancellationToken)
        {
            // Add subscribers (idempotent)
            await _subscriptionService.AddDefaultSubscribersAsync(notification.TicketId, notification.DepartmentId, notification.CreatedById);

            // Publish a realtime/DB notification (non-blocking best-effort)
            try
            {
                var message = _localizer["A new ticket has been created."];
                await _notificationPublisher.PublishTicketUpdatedAsync(notification.TicketId, message);
            }
            catch (System.Exception ex)
            {
                // Log and swallow — event handlers should avoid crashing pipeline
                _logger.LogError(ex, "Failed to publish notification for ticket {TicketId}", notification.TicketId);
            }
        }
    }
}
