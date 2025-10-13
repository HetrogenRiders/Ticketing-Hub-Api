using MediatR;

namespace TicketingHub.Api.Features.Admin.Configuration.StatusManagement.UpdateStatus
{
    public class UpdateStatusRequest : IRequest<UpdateStatusResponse> 
    {
        public Guid StatusId { get; set; }
        public string? StatusName { get; set; }
        public int SortOrder { get; set; }
        public string? ColorCode { get; set; }
        public bool IsFinalStatus { get; set; }
        public bool IsActive { get; set; }
    }
}
