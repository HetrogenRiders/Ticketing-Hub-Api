using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketingHub.Api.Features.ResetPassword;
using TicketingHub.Api.Features.ChangePassword;
using TicketingHub.Api.Common;

namespace TicketingHub.Api.Controllers;

[AllowAnonymous]
public class ResetPasswordController : ApiControllerBase
{
    [HttpPost("reset-password", Name = "ResetPassword")]
    public async Task<ActionResult<ResetPasswordResponse>> ResetPasswordAsync(ResetPasswordRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpPost("change-password", Name = "ChangePassword")]
    public async Task<ActionResult<ChangePasswordResponse>> ChangePasswordAsync(ChangePasswordRequest request)
    {
        return await Mediator.Send(request);
    }
}
