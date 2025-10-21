using MediatR;

namespace TicketingHub.Api.Features.Ticketing.NotificationManagement.MarkAsRead
{
    public class MarkAsReadRequest : IRequest<MarkAsReadResponse>
    {
        public Guid NotificationId { get; set; }
    }
}
