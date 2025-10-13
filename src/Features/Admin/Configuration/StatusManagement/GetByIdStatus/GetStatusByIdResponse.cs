namespace TicketingHub.Api.Features.Admin.Configuration.StatusManagement.GetByIdStatus
{
    public class GetStatusByIdResponse
    {
        public string? StatusName { get; set; }
        public int SortOrder { get; set; }
        public string? ColorCode { get; set; }
        public bool IsFinalStatus { get; set; }
        public bool IsActive { get; set; }
    }
}
