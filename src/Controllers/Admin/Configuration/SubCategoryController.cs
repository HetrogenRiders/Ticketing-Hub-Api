using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketingHub.Api.Common;
using TicketingHub.Api.Features.Admin.Configuration.SubCategoryManagement.CreateSubCategory;
using TicketingHub.Api.Features.Admin.Configuration.SubCategoryManagement.UpdateSubCategory;
using TicketingHub.Api.Features.Admin.Configuration.SubCategoryManagement.DeleteSubCategory;
using TicketingHub.Api.Features.Admin.Configuration.SubCategoryManagement.GetAllSubCategory;
using TicketingHub.Api.Features.Admin.Configuration.SubCategoryManagement.GetByIdSubCategory;

namespace TicketingHub.Api.Controllers.Admin.Configuration
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/config/subcategories")]
    public class SubCategoryController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetAllSubCategoryResponse>> GetAll()
        {
            return await Mediator.Send(new GetAllSubCategoryRequest());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetSubCategoryByIdResponse>> GetById(Guid id)
        {
            return await Mediator.Send(new GetSubCategoryByIdRequest { SubCategoryId = id });
        }

        [HttpPost]
        public async Task<ActionResult<CreateSubCategoryResponse>> Create([FromBody] CreateSubCategoryRequest request)
        {
            return await Mediator.Send(request);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UpdateSubCategoryResponse>> Update(Guid id, [FromBody] UpdateSubCategoryRequest request)
        {
            request.SubCategoryId = id;
            return await Mediator.Send(request);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<DeleteSubCategoryResponse>> Delete(Guid id)
        {
            return await Mediator.Send(new DeleteSubCategoryRequest { SubCategoryId = id });
        }
    }
}
