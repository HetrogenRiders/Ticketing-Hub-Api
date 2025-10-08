using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketingHub.Api.Common;
using TicketingHub.Api.Features.UserManagement;
using TicketingHub.Api.Filters;

namespace TicketingHub.Api.Controllers;

[Authorize]
public class UserController : ApiControllerBase
{
    [HttpGet(Name = "GetAllUsers")]
    [PermissionRequirement("CanView")]
    public async Task<ActionResult<GetAllUsersResponse>> GetAllUsersQuery([FromQuery] GetAllUsersRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet("{id:guid}", Name = "GetUser")]
    [PermissionRequirement("CanView")]
    public async Task<ActionResult<GetUserResponse>> GetAsync(Guid id)
    {
        var request = new GetUserRequest { UserId = id };
        return await Mediator.Send(request);
    }

    [HttpPost(Name = "CreateUser")]
    [PermissionRequirement("CanAdd")]
    public async Task<ActionResult<CreateUserResponse>> CreateAsync(CreateUserRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut(Name = "EditUser")]
    [PermissionRequirement("CanEdit")]
    public async Task<ActionResult<EditUserResponse>> EditAsync(EditUserRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpDelete("{id:guid}", Name = "DeleteUser")]
    [PermissionRequirement("CanDelete")]
    public async Task<ActionResult<DeleteUserResponse>> DeleteAsync(Guid id)
    {
        var request = new DeleteUserRequest { UserId = id };
        return await Mediator.Send(request);
    }
}