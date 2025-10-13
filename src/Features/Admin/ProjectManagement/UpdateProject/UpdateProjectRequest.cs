using MediatR;

namespace TicketingHub.Api.Features.Admin.ProjectManagement.UpdateProject
{
    public class UpdateProjectRequest : IRequest<UpdateProjectResponse>
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public Guid DepartmentId { get; set; }
        public Guid ManagerId { get; set; }
        public bool IsActive { get; set; }
    }
}
