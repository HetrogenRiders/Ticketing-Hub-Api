namespace TicketingHub.Api.Features.Admin.ProjectManagement.GetAllProjects
{ 

    public class GetAllProjectsResponse
    {
        public List<ProjectDto> Projects { get; set; } = new();
    }
}
