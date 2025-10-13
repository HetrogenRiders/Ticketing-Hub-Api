using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketingHub.Api.Common;
using TicketingHub.Api.Features.Admin.Configuration.SLAManagement.CreateSLA;
using TicketingHub.Api.Features.Admin.Configuration.SLAManagement.UpdateSLA;
using TicketingHub.Api.Features.Admin.Configuration.SLAManagement.DeleteSLA;
using TicketingHub.Api.Features.Admin.Configuration.SLAManagement.GetAllSLA;
using TicketingHub.Api.Features.Admin.Configuration.SLAManagement.GetByIdSLA;

namespace TicketingHub.Api.Controllers.Admin.Configuration
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/config/slas")]
    public class SLAController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetAllSLAResponse>> GetAll()
        {
            return await Mediator.Send(new GetAllSLARequest());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetSLAByIdResponse>> GetById(Guid id)
        {
            return await Mediator.Send(new GetSLAByIdRequest { SLAId = id });
        }

        [HttpPost]
        public async Task<ActionResult<CreateSLAResponse>> Create([FromBody] CreateSLARequest request)
        {
            return await Mediator.Send(request);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UpdateSLAResponse>> Update(Guid id, [FromBody] UpdateSLARequest request)
        {
            request.SLAId = id;
            return await Mediator.Send(request);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<DeleteSLAResponse>> Delete(Guid id)
        {
            return await Mediator.Send(new DeleteSLARequest { SLAId = id });
        }
    }
}
