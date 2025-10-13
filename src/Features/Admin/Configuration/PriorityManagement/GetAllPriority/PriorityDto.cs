namespace TicketingHub.Api.Features.Admin.Configuration.PriorityManagement.GetAllPriority
{
    public class PriorityDto
    {
        public Guid Id { get; set; }
        public string PriorityName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}