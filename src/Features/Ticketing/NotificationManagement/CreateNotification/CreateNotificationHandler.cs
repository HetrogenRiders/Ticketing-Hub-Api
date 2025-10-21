using MediatR;
using Microsoft.Extensions.Localization;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Resources;
using TicketingHub.Api.Domain;
using TicketingHub.Api.Common.Interfaces;
using Microsoft.Extensions.Logging;
using TicketingHub.Api.Infrastructure.Services;

namespace TicketingHub.Api.Features.Ticketing.NotificationManagement.CreateNotification
{
    public class CreateNotificationHandler : IRequestHandler<CreateNotificationRequest, CreateNotificationResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly Common.Interfaces.INotificationPublisher _notificationPublisher;
        private readonly ILogger<CreateNotificationHandler> _logger;

        public CreateNotificationHandler(
            DBContext context,
            IStringLocalizer<SharedResource> localizer,
            Common.Interfaces.INotificationPublisher notificationPublisher,
            ILogger<CreateNotificationHandler> logger)
        {
            _context = context;
            _localizer = localizer;
            _notificationPublisher = notificationPublisher;
            _logger = logger;
        }

        public async Task<CreateNotificationResponse> Handle(CreateNotificationRequest request, CancellationToken cancellationToken)
        {
            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                TicketId = request.TicketId,
                RecipientId = request.RecipientId,
                Message = request.Message,
                NotificationType = request.NotificationType,
                IsRead = false,
                Created = DateTime.UtcNow
            };

            await _context.Notifications.AddAsync(notification, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            try
            {
                // Try real-time push (non-blocking)
                await _notificationPublisher.PublishTicketUpdatedAsync(notification.TicketId ?? Guid.Empty, request.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending real-time notification for {NotificationId}", notification.Id);
            }

            return new CreateNotificationResponse
            {
                NotificationId = notification.Id,
                Message = _localizer["Notification created successfully."],
                IsSent = true
            };
        }
    }
}
