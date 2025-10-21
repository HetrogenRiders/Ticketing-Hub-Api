using MediatR;
using System;

namespace TicketingHub.Api.Features.Ticketing.NotificationManagement.CreateNotification
{
    public class CreateNotificationRequest : IRequest<CreateNotificationResponse>
    {
        public Guid? TicketId { get; set; }             // Related ticket (optional)
        public Guid RecipientId { get; set; }           // User who will receive it
        public string Message { get; set; } = string.Empty;
        public string NotificationType { get; set; } = "Info"; // Info / Alert / SLA / Comment etc.
    }
}
