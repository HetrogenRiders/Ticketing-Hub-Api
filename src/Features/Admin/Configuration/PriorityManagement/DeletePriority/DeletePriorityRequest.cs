using MediatR;

namespace TicketingHub.Api.Features.Admin.Configuration.PriorityManagement.DeletePriority
{
    public class DeletePriorityRequest : IRequest<DeletePriorityResponse> 
    {
        public Guid PriorityId { get; set; }
    }
}
