using MediatR;

namespace TicketingHub.Api.Features.Admin.Configuration.SLAManagement.DeleteSLA
{
    public class DeleteSLARequest : IRequest<DeleteSLAResponse> 
    {
        public Guid SLAId { get; set; }
    }
}
