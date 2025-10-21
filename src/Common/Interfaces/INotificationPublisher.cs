

namespace TicketingHub.Api.Common.Interfaces
{
    public interface INotificationPublisher
    {
        Task PublishTicketSlaBreachAsync(Guid ticketId, string message);
        Task PublishTicketUpdatedAsync(Guid ticketId, string message);
    }
}
