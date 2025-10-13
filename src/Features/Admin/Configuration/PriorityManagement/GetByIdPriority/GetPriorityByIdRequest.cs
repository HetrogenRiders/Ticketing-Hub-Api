using MediatR;

namespace TicketingHub.Api.Features.Admin.Configuration.PriorityManagement.GetByIdPriority
{
    public class GetPriorityByIdRequest : IRequest<GetPriorityByIdResponse> 
    {
        public Guid PriorityId { get; set; }
    }
}
