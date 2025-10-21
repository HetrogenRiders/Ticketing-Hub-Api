using System.Collections.Generic;

namespace TicketingHub.Api.Features.Ticketing.NotificationManagement.GetUserNotifications
{
    public class GetUserNotificationsResponse
    {
        public List<UserNotificationDto> Notifications { get; set; } = new();
    }

   
}
