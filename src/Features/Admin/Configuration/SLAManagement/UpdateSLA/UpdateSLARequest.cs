using MediatR;

namespace TicketingHub.Api.Features.Admin.Configuration.SLAManagement.UpdateSLA
{
    public class UpdateSLARequest : IRequest<UpdateSLAResponse> 
    {
        public Guid SLAId { get; set; }
        public string? SLAName { get; set; }
        public int ResponseTimeInHours { get; set; }
        public int ResolutionTimeInHours { get; set; }
        public int EscalationLevel1Hours { get; set; }
        public int EscalationLevel2Hours { get; set; }
        public bool IsActive { get; set; }
    }
}
