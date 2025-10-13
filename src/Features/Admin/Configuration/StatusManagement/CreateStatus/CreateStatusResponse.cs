namespace TicketingHub.Api.Features.Admin.Configuration.StatusManagement.CreateStatus
{
    public class CreateStatusResponse
    {
        public Guid StatusId { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
