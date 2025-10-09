using TicketingHub.Api.Common;

namespace TicketingHub.Api.Domain
{
    public class Status : AuditableEntity
    {
        public Guid Id { get; set; } // StatusId
        public string StatusName { get; set; } = string.Empty;
        public int SortOrder { get; set; }
        public string? ColorCode { get; set; }
        public bool IsFinalStatus { get; set; } = false;
        public bool IsActive { get; set; } = true;

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
