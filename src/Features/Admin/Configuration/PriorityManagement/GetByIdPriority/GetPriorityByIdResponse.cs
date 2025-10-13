namespace TicketingHub.Api.Features.Admin.Configuration.PriorityManagement.GetByIdPriority
{
    public class GetPriorityByIdResponse
    {
        public string? PriorityName { get; set; }
        public int SLAHours { get; set; }
        public string? ColorCode { get; set; }
        public bool IsActive { get; set; }
    }
}
