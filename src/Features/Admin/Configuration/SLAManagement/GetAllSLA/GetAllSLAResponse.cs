namespace TicketingHub.Api.Features.Admin.Configuration.SLAManagement.GetAllSLA
{
    public class GetAllSLAResponse
    {
        // Use DTO definition file for response
        public List<SLADto> SLADtos { get; set; } = new();
    }
}
