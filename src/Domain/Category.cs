using TicketingHub.Api.Common;

namespace TicketingHub.Api.Domain
{
    public class Category : AuditableEntity
    {
        public Guid Id { get; set; } // CategoryId
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
