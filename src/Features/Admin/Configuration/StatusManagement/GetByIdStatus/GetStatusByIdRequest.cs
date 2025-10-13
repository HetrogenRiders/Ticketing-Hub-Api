using MediatR;

namespace TicketingHub.Api.Features.Admin.Configuration.StatusManagement.GetByIdStatus
{
    public class GetStatusByIdRequest : IRequest<GetStatusByIdResponse> 
    {
        public Guid StatusId { get; set; }
    }
}
