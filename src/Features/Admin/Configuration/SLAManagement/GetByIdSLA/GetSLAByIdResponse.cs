namespace TicketingHub.Api.Features.Admin.Configuration.SLAManagement.GetByIdSLA
{
    public class GetSLAByIdResponse
    {
        public string? SLAName { get; set; }
        public int ResponseTimeInHours { get; set; }
        public int ResolutionTimeInHours { get; set; }
        public int EscalationLevel1Hours { get; set; }
        public int EscalationLevel2Hours { get; set; }
        public bool IsActive { get; set; }
    }
}
