using MediatR;

namespace TicketingHub.Api.Features.Admin.ProjectManagement.DeleteProject
{
    public class DeleteProjectRequest : IRequest<DeleteProjectResponse>
    {
        public Guid ProjectId { get; set; }
    }
}
