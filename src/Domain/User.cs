
using TicketingHub.Api.Common;

namespace TicketingHub.Api.Domain
{
    public class User : AuditableEntity
    {
        public Guid Id { get; set; } // UserId
        public string EmployeeCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PasswordHash { get; set; }
        public Guid? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public Guid? RoleId { get; set; }
        public Role? Role { get; set; }
        public Guid? ManagerId { get; set; }
        public User? Manager { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public ICollection<User> TeamMembers { get; set; } = new List<User>();
        public ICollection<Ticket> CreatedTickets { get; set; } = new List<Ticket>();
        public ICollection<Ticket> AssignedTickets { get; set; } = new List<Ticket>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
