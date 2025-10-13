using MediatR;

namespace TicketingHub.Api.Features.Admin.Configuration.SLAManagement.GetByIdSLA
{
    public class GetSLAByIdRequest : IRequest<GetSLAByIdResponse> 
    {
        public Guid SLAId { get; set; }
    }
}
