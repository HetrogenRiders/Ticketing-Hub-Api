using System.Collections.Generic;

namespace TicketingHub.Api.Features.Admin.RoleManagement.GetAllRoles
{
    /// <summary>
    /// Response model for returning all roles.
    /// </summary>
    public class GetAllRolesResponse
    {
        public List<RoleDto> Roles { get; set; } = new();
    }
}
