using TicketingHub.Api.Common;

namespace TicketingHub.Api.Domain
{
    public class Department : AuditableEntity
    {
        public Guid Id { get; set; } // DepartmentId
        public string DepartmentName { get; set; } = string.Empty;
        public Guid? ParentDepartmentId { get; set; }
        public Department? ParentDepartment { get; set; }
        public Guid? HeadUserId { get; set; }
        public User? HeadUser { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Project> Projects { get; set; } = new List<Project>();

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
