using System;
using System.Collections.Generic;
using TicketingHub.Api.Common;

namespace TicketingHub.Api.Domain
{
    public class SubCategory : AuditableEntity
    {
        public Guid Id { get; set; } // SubCategoryId
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public string SubCategoryName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
