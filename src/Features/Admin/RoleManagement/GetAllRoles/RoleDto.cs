namespace TicketingHub.Api.Features.Admin.RoleManagement.GetAllRoles
{
    /// <summary>
    /// Data transfer object representing a role record.
    /// </summary>
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
