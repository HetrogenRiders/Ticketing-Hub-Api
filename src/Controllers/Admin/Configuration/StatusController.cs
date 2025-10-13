using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketingHub.Api.Common;
using TicketingHub.Api.Features.Admin.Configuration.StatusManagement.CreateStatus;
using TicketingHub.Api.Features.Admin.Configuration.StatusManagement.UpdateStatus;
using TicketingHub.Api.Features.Admin.Configuration.StatusManagement.DeleteStatus;
using TicketingHub.Api.Features.Admin.Configuration.StatusManagement.GetAllStatus;
using TicketingHub.Api.Features.Admin.Configuration.StatusManagement.GetByIdStatus;

namespace TicketingHub.Api.Controllers.Admin.Configuration
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/config/statuses")]
    public class StatusController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetAllStatusResponse>> GetAll()
        {
            return await Mediator.Send(new GetAllStatusRequest());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetStatusByIdResponse>> GetById(Guid id)
        {
            return await Mediator.Send(new GetStatusByIdRequest { StatusId = id });
        }

        [HttpPost]
        public async Task<ActionResult<CreateStatusResponse>> Create([FromBody] CreateStatusRequest request)
        {
            return await Mediator.Send(request);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UpdateStatusResponse>> Update(Guid id, [FromBody] UpdateStatusRequest request)
        {
            request.StatusId = id;
            return await Mediator.Send(request);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<DeleteStatusResponse>> Delete(Guid id)
        {
            return await Mediator.Send(new DeleteStatusRequest { StatusId = id });
        }
    }
}
