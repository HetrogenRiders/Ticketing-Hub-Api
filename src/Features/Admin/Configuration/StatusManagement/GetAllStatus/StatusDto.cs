namespace TicketingHub.Api.Features.Admin.Configuration.StatusManagement.GetAllStatus
{
    public class StatusDto
    {
        public Guid Id { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}