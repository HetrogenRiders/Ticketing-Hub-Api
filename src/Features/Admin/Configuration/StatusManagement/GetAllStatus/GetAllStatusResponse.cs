namespace TicketingHub.Api.Features.Admin.Configuration.StatusManagement.GetAllStatus
{
    public class GetAllStatusResponse
    {
        // Use DTO definition file for response
        public List<StatusDto> StatusDtos { get; set; } = new();
    }
}
