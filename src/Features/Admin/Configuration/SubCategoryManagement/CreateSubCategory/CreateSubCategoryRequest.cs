using MediatR;

namespace TicketingHub.Api.Features.Admin.Configuration.SubCategoryManagement.CreateSubCategory
{
    public class CreateSubCategoryRequest : IRequest<CreateSubCategoryResponse> 
    {
        public Guid CategoryId { get; set; }
        public string? SubCategoryName { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Description { get; set; }
    }
}
