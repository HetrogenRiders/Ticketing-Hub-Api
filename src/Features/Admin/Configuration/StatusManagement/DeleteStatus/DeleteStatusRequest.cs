using MediatR;

namespace TicketingHub.Api.Features.Admin.Configuration.StatusManagement.DeleteStatus
{
    public class DeleteStatusRequest : IRequest<DeleteStatusResponse> 
    {
        public Guid StatusId { get; set; }
    }
}
