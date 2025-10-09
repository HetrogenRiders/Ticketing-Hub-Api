using System;
using System.Collections.Generic;
using TicketingHub.Api.Common;

namespace TicketingHub.Api.Domain
{
    public class Project : AuditableEntity
    {
        public Guid Id { get; set; } // ProjectId
        public string ProjectName { get; set; } = string.Empty;
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        public Guid? ManagerId { get; set; }
        public User? Manager { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
