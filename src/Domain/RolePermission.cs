using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketingHub.Api.Domain
{
    [Table("RolePermissions")]
    public class RolePermission
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        [Required]
        public Guid ModuleId { get; set; }

        [Required]
        public Guid PermissionTypeId { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        [MaxLength(200)]
        public string? CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        [MaxLength(200)]
        public string? LastModifiedBy { get; set; }

        // 🔗 Navigation properties
        public virtual Role Role { get; set; } = null!;
        public virtual PermissionType PermissionType { get; set; } = null!;
    }
}
