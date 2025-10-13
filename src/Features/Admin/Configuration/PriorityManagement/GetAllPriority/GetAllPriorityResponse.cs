namespace TicketingHub.Api.Features.Admin.Configuration.PriorityManagement.GetAllPriority
{
    public class GetAllPriorityResponse
    {
        // Use DTO definition file for response
        public List<PriorityDto> PriorityDtos { get; set; } = new();
    }
}
