using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketingHub.Api.Common;
using TicketingHub.Api.Features.Admin.RoleManagement.CreateRole;
using TicketingHub.Api.Features.Admin.RoleManagement.DeleteRole;
using TicketingHub.Api.Features.Admin.RoleManagement.GetAllRoles;
using TicketingHub.Api.Features.Admin.RoleManagement.GetRoleById;
using TicketingHub.Api.Features.Admin.RoleManagement.UpdateRole;

namespace TicketingHub.Api.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/roles")]
    public class RoleAdminController : ApiControllerBase
    {
        [HttpPost("create")]
        public async Task<ActionResult<CreateRoleCommandResponse>> Create([FromBody] CreateRoleCommandRequest request)
        {
            return await Mediator.Send(request);
        }

        [HttpPut("update/{id:guid}")]
        public async Task<ActionResult<UpdateRoleCommandResponse>> Update(Guid id, [FromBody] UpdateRoleCommandRequest request)
        {
            request.RoleId = id;
            return await Mediator.Send(request);
        }

        [HttpDelete("delete/{id:guid}")]
        public async Task<ActionResult<DeleteRoleCommandResponse>> Delete(Guid id)
        {
            var request = new DeleteRoleCommandRequest { RoleId = id };
            return await Mediator.Send(request);
        }

        [HttpGet("list")]
        public async Task<ActionResult<GetAllRolesResponse>> GetAll()
        {
            var request = new GetAllRolesRequest();
            return await Mediator.Send(request);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetRoleByIdResponse>> GetById(Guid id)
        {
            var request = new GetRoleByIdRequest { RoleId = id };
            return await Mediator.Send(request);
        }

    }
}
