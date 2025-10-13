using MediatR;

namespace TicketingHub.Api.Features.Admin.ProjectManagement.CreateProject
{
    public class CreateProjectRequest : IRequest<CreateProjectResponse>
    {
        public string ProjectName { get; set; } = string.Empty;
        public Guid DepartmentId { get; set; }
        public Guid ManagerId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
