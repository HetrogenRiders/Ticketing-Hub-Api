using MediatR;

namespace TicketingHub.Api.Features.Admin.Configuration.SLAManagement.CreateSLA
{
    public class CreateSLARequest : IRequest<CreateSLAResponse> 
    {
        public string? SLAName { get; set; }
        public int ResponseTimeInHours { get; set; }
        public int ResolutionTimeInHours { get; set; }
        public int EscalationLevel1Hours { get; set; }
        public int EscalationLevel2Hours { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
