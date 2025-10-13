namespace TicketingHub.Api.Features.Admin.Configuration.PriorityManagement.CreatePriority
{
    public class CreatePriorityResponse
    {
        public Guid PriorityId { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
