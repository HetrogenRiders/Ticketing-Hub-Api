using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketingHub.Api.Common;
using TicketingHub.Api.Features.Authentication.ExternalLogin;

namespace TicketingHub.Api.Controllers;

[AllowAnonymous] // Allow SSO login without JWT
public class ExternalLoginController : ApiControllerBase
{
    /// <summary>
    /// Performs external SSO login (e.g., Azure AD, Google, Okta)
    /// </summary>
    [HttpPost("sso")]
    public async Task<ActionResult<ExternalLoginResponse>> ExternalLoginAsync([FromBody] ExternalLoginCommandRequest request)
    {
        // Mediator automatically triggers validator and handler
        return await Mediator.Send(request);
    }
}
