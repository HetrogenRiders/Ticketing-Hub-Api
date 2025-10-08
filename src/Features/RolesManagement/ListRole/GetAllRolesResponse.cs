namespace TicketingHub.Api.Features.RolesManagement
{
    public class GetAllRolesResponse
    {
        public IEnumerable<RoleDto> Roles { get; set; }
    }

    public class RoleDto
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public IEnumerable<ModuleConfiguration> Modules { get; set; }
    }
}
