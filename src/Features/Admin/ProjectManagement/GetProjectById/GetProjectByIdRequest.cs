using MediatR;

namespace TicketingHub.Api.Features.Admin.ProjectManagement.GetProjectById
{
    public class GetProjectByIdRequest : IRequest<GetProjectByIdResponse>
    {
        public Guid ProjectId { get; set; }
    }
}
