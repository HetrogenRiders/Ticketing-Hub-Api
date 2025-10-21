using MediatR;

namespace TicketingHub.Api.Features.Ticketing.NotificationManagement.GetUserNotifications
{
    public class GetUserNotificationsRequest : IRequest<GetUserNotificationsResponse>
    {
        public Guid UserId { get; set; }
    }
}
