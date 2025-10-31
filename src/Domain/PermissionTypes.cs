using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketingHub.Api.Domain
{
    [Table("PermissionTypes")]
    public class PermissionType
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(200)]
        public string? PermissionName { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }

        // 🔗 Navigation property
        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
