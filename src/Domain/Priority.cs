using TicketingHub.Api.Common;

namespace TicketingHub.Api.Domain
{
    public class Priority : AuditableEntity
    {
        public Guid Id { get; set; } // PriorityId
        public string PriorityName { get; set; } = string.Empty;
        public int SLAHours { get; set; }
        public string? ColorCode { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
