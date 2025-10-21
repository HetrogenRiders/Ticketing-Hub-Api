using MediatR;
using System;

namespace TicketingHub.Api.Features.Ticketing.TicketManagement.CreateTicket
{
    public class CreateTicketRequest : IRequest<CreateTicketResponse>
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid CreatedById { get; set; }               // who raised the ticket
        public Guid? AssignedToId { get; set; }             // optional initial assignee
        public Guid DepartmentId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public Guid PriorityId { get; set; }
        public Guid StatusId { get; set; }
        public Guid? SLAId { get; set; }                    // optional SLA to compute DueDate
        public DateTime? DueDate { get; set; }              // optional override
    }
}
