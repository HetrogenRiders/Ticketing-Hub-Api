using System;

namespace TicketingHub.Api.Features.Ticketing.NotificationManagement.CreateNotification
{
    public class CreateNotificationResponse
    {
        public Guid NotificationId { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsSent { get; set; }
    }
}
