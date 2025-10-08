using TicketingHub.Api.Features.RolesManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketingHub.Api.Common;
using TicketingHub.Api.Filters;

namespace TicketingHub.Api.Controllers;

[Authorize]
public class RoleController : ApiControllerBase
{
    [HttpGet(Name = "GetAllRoles")]
    [PermissionRequirement("CanView")]
    public async Task<ActionResult<GetAllRolesResponse>> GetAllRolesQuery([FromQuery] GetAllRolesRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet("{id:guid}", Name = "GetRole")]
    [PermissionRequirement("CanView")]
    public async Task<ActionResult<GetRoleResponse>> GetAsync(Guid id)
    {
        var request = new GetRoleRequest { RoleId = id };
        return await Mediator.Send(request);
    }

    [HttpPost(Name = "CreateRole")]
    [PermissionRequirement("CanAdd")]
    public async Task<ActionResult<CreateRoleResponse>> CreateAsync(CreateRoleRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut(Name = "EditRole")]
    [PermissionRequirement("CanEdit")]
    public async Task<ActionResult<EditRoleResponse>> EditAsync(EditRoleRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpDelete("{id:guid}", Name = "DeleteRole")]
    [PermissionRequirement("CanDelete")]
    public async Task<ActionResult<DeleteRoleResponse>> DeleteAsync(Guid id)
    {
        var request = new DeleteRoleRequest { RoleId = id };
        return await Mediator.Send(request);
    }
}