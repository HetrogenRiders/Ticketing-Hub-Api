using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TicketingHub.Api.Features.Authentication.InternalLogin;

namespace TicketingHub.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("InternalLogin")]
        public async Task<IActionResult> Login([FromBody] InternalLoginRequest model)
        {
            var request = new InternalLoginRequest
            {
                Email = model.Email,
                Password = model.Password
            };

            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
