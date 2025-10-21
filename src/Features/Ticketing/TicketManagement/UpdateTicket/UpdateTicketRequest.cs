using MediatR;
using System;

namespace TicketingHub.Api.Features.Ticketing.TicketManagement.UpdateTicket
{
    public class UpdateTicketRequest : IRequest<UpdateTicketResponse>
    {
        public Guid TicketId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid? AssignedToId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public Guid PriorityId { get; set; }
        public Guid StatusId { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid UpdatedById { get; set; }
    }
}
