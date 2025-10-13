using MediatR;

namespace TicketingHub.Api.Features.Admin.Configuration.PriorityManagement.UpdatePriority
{
    public class UpdatePriorityRequest : IRequest<UpdatePriorityResponse> 
    {
        public Guid PriorityId { get; set; }
        public string? PriorityName { get; set; }
        public int SLAHours { get; set; }
        public string? ColorCode { get; set; }
        public bool IsActive { get; set; }
    }
}
