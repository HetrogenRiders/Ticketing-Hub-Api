using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketingHub.Api.Common;
using TicketingHub.Api.Features.ModuleManagement;

namespace TicketingHub.Api.Controllers;

[Authorize]
public class ModuleController : ApiControllerBase
{
    [HttpGet(Name = "GetAllModules")]
    [Authorize(Roles = "SuperAdmin")]  // Restrict access to SuperAdmin only
    public async Task<ActionResult<GetAllModulesResponse>> GetAllModulesQuery([FromQuery] GetAllModulesRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet("user/{id:guid}", Name = "GetModulesByUserId")]
    public async Task<ActionResult<GetUserModulesByUserIDResponse>> GetRoleByUserIdAsync(Guid id)
    {
        var request = new GetUserModulesByUserIDRequest { UserId = id };
        return await Mediator.Send(request);
    }
}