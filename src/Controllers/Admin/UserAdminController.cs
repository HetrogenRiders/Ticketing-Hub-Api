using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketingHub.Api.Common;
using TicketingHub.Api.Features.Admin.UserManagement.CreateUser;
using TicketingHub.Api.Features.Admin.UserManagement.UpdateUser;
using TicketingHub.Api.Features.Admin.UserManagement.DeleteUser;
using TicketingHub.Api.Features.Admin.UserManagement.GetAllUsers;

namespace TicketingHub.Api.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/users")]
    public class UserAdminController : ApiControllerBase
    {
        [HttpPost("create")]
        public async Task<ActionResult<CreateUserResponse>> Create([FromBody] CreateUserRequest request)
        {
            return await Mediator.Send(request);
        }

        [HttpPut("update/{id:guid}")]
        public async Task<ActionResult<UpdateUserResponse>> Update(Guid id, [FromBody] UpdateUserRequest request)
        {
            request.UserId = id;
            return await Mediator.Send(request);
        }

        [HttpDelete("delete/{id:guid}")]
        public async Task<ActionResult<DeleteUserResponse>> Delete(Guid id)
        {
            var request = new DeleteUserRequest { UserId = id };
            return await Mediator.Send(request);
        }

        [HttpGet("list")]
        public async Task<ActionResult<GetAllUsersResponse>> GetAll()
        {
            var request = new GetAllUsersRequest();
            return await Mediator.Send(request);
        }
    }
}
