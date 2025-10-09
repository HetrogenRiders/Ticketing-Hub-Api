using System;
using System.Collections.Generic;
using TicketingHub.Api.Common;

namespace TicketingHub.Api.Domain
{
    public class Role : AuditableEntity
    {
        public Guid Id { get; set; } // RoleId
        public string RoleName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsDeleted { get; set; } = false;

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
