namespace TicketingHub.Api.Features.Ticketing.NotificationManagement.GetUserNotifications
{
    public class UserNotificationDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
