using System;
using System.Threading.Tasks;

namespace TicketingHub.Api.Common.Interfaces
{
    public interface ITicketAssignmentService
    {
        /// <summary>
        /// Automatically assigns a ticket to the department head.
        /// </summary>
        Task AssignToDepartmentHeadAsync(Guid ticketId, Guid departmentId);
    }
}
