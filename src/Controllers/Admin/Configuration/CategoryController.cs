//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using TicketingHub.Api.Common;
//using TicketingHub.Api.Features.Admin.Configuration.CategoryManagement.CreateCategory;
//using TicketingHub.Api.Features.Admin.Configuration.CategoryManagement.UpdateCategory;
//using TicketingHub.Api.Features.Admin.Configuration.CategoryManagement.DeleteCategory;
//using TicketingHub.Api.Features.Admin.Configuration.CategoryManagement.GetAllCategories;
//using TicketingHub.Api.Features.Admin.Configuration.CategoryManagement.GetCategoryById;

//namespace TicketingHub.Api.Controllers.Admin.Configuration
//{
//    [Authorize(Roles = "Admin")]
//    [Route("api/admin/config/categories")]
//    public class CategoryController : ApiControllerBase
//    {
//        [HttpGet]
//        public async Task<ActionResult<GetAllCategoriesResponse>> GetAll()
//        {
//            return await Mediator.Send(new GetAllCategoriesRequest());
//        }

//        [HttpGet("{id:guid}")]
//        public async Task<ActionResult<GetCategoryByIdResponse>> GetById(Guid id)
//        {
//            return await Mediator.Send(new GetCategoryByIdRequest { CategoryId = id });
//        }

//        [HttpPost]
//        public async Task<ActionResult<CreateCategoryResponse>> Create([FromBody] CreateCategoryRequest request)
//        {
//            return await Mediator.Send(request);
//        }

//        [HttpPut("{id:guid}")]
//        public async Task<ActionResult<UpdateCategoryResponse>> Update(Guid id, [FromBody] UpdateCategoryRequest request)
//        {
//            request.CategoryId = id;
//            return await Mediator.Send(request);
//        }

//        [HttpDelete("{id:guid}")]
//        public async Task<ActionResult<DeleteCategoryResponse>> Delete(Guid id)
//        {
//            return await Mediator.Send(new DeleteCategoryRequest { CategoryId = id });
//        }
//    }
//}
