namespace TicketingHub.Api.Features.Admin.ProjectManagement.CreateProject
{
    public class CreateProjectResponse
    {
        public Guid ProjectId { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
