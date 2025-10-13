using MediatR;

namespace TicketingHub.Api.Features.Admin.Configuration.StatusManagement.CreateStatus
{
    public class CreateStatusRequest : IRequest<CreateStatusResponse> 
    {
        public string? StatusName { get; set; }
        public int SortOrder { get; set; }
        public string? ColorCode { get; set; }
        public bool IsFinalStatus { get; set; } = true;
        public bool IsActive { get; set; } = true;
    }
}
