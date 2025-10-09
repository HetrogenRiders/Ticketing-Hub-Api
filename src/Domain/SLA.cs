using TicketingHub.Api.Common;

namespace TicketingHub.Api.Domain
{
    public class SLA : AuditableEntity
    {
        public Guid Id { get; set; } // SLAId
        public string SLAName { get; set; } = string.Empty;
        public int ResponseTimeInHours { get; set; }
        public int ResolutionTimeInHours { get; set; }
        public int? EscalationLevel1Hours { get; set; }
        public int? EscalationLevel2Hours { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
