using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketingHub.Api.Common;
using TicketingHub.Api.Features.Admin.Configuration.PriorityManagement.CreatePriority;
using TicketingHub.Api.Features.Admin.Configuration.PriorityManagement.UpdatePriority;
using TicketingHub.Api.Features.Admin.Configuration.PriorityManagement.DeletePriority;
using TicketingHub.Api.Features.Admin.Configuration.PriorityManagement.GetAllPriority;
using TicketingHub.Api.Features.Admin.Configuration.PriorityManagement.GetByIdPriority;

namespace TicketingHub.Api.Controllers.Admin.Configuration
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/config/priorities")]
    public class PriorityController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetAllPriorityResponse>> GetAll()
        {
            return await Mediator.Send(new GetAllPriorityRequest());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetPriorityByIdResponse>> GetById(Guid id)
        {
            return await Mediator.Send(new GetPriorityByIdRequest { PriorityId = id });
        }

        [HttpPost]
        public async Task<ActionResult<CreatePriorityResponse>> Create([FromBody] CreatePriorityRequest request)
        {
            return await Mediator.Send(request);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UpdatePriorityResponse>> Update(Guid id, [FromBody] UpdatePriorityRequest request)
        {
            request.PriorityId = id;
            return await Mediator.Send(request);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<DeletePriorityResponse>> Delete(Guid id)
        {
            return await Mediator.Send(new DeletePriorityRequest { PriorityId = id });
        }
    }
}
