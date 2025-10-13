namespace TicketingHub.Api.Features.Admin.Configuration.SLAManagement.GetAllSLA
{
    public class SLADto
    {
        public Guid Id { get; set; }
        public string SLAName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}