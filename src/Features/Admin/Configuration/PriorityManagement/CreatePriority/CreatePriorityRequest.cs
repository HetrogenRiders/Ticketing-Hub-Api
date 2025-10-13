using MediatR;

namespace TicketingHub.Api.Features.Admin.Configuration.PriorityManagement.CreatePriority
{
    public class CreatePriorityRequest : IRequest<CreatePriorityResponse> 
    {
        public string? PriorityName { get; set; }
        public int SLAHours { get; set; }
        public string? ColorCode { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
