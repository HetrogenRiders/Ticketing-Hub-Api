using TicketingHub.Api.Common;

namespace TicketingHub.Api.Domain
{
    public class Ticket : AuditableEntity
    {
        public Guid Id { get; set; } // TicketId
        public string TicketNumber { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public Guid CreatedById { get; set; }
        public User CreatedByUser { get; set; } = null!;
        public Guid? AssignedToId { get; set; }
        public User? AssignedTo { get; set; }
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        public Guid? ProjectId { get; set; }
        public Project? Project { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public Guid? SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }
        public Guid PriorityId { get; set; }
        public Priority Priority { get; set; } = null!;
        public Guid StatusId { get; set; }
        public Status Status { get; set; } = null!;
        public Guid? SLAId { get; set; }
        public SLA? SLA { get; set; }

        public DateTime? DueDate { get; set; }
        public DateTime? ClosedAt { get; set; }
        public bool IsEscalated { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        public ICollection<TicketComment> Comments { get; set; } = new List<TicketComment>();
        public ICollection<TicketAttachment> Attachments { get; set; } = new List<TicketAttachment>();
        public ICollection<TicketHistory> History { get; set; } = new List<TicketHistory>();
    }
}
