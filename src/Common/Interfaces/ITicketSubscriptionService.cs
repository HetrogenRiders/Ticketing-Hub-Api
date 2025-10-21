using System;
using System.Threading.Tasks;

namespace TicketingHub.Api.Common.Interfaces
{
    public interface ITicketSubscriptionService
    {
        /// <summary>
        /// Add default subscribers for the ticket (creator, assignee, department users, etc.)
        /// Must be idempotent (safe to call multiple times).
        /// </summary>
        Task AddDefaultSubscribersAsync(Guid ticketId, Guid departmentId, Guid createdById);
    }
}
