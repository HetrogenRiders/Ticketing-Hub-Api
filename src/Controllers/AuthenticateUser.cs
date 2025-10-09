using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketingHub.Api.Common;
using TicketingHub.Api.Features.Authentication;

namespace TicketingHub.Api.Controllers;

[AllowAnonymous]
public class AuthenticateUserController : ApiControllerBase
{
    //[HttpPost(Name = "LoginUser")]
    //public async Task<ActionResult<AuthenticateUserResponse>> LoginAsync(AuthenticateUserRequest request)
    //{
    //    return await Mediator.Send(request);
    //}

    //[HttpPost("refresh-token")]
    //public async Task<ActionResult<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    //{
    //    return await Mediator.Send(request);
    //}
}