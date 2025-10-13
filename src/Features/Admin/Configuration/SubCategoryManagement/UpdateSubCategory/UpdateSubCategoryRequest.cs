using MediatR;

namespace TicketingHub.Api.Features.Admin.Configuration.SubCategoryManagement.UpdateSubCategory
{
    public class UpdateSubCategoryRequest : IRequest<UpdateSubCategoryResponse> 
    {
        public Guid SubCategoryId { get; set; }
        public Guid CategoryId { get; set; }
        public string? SubCategoryName { get; set; }
        public bool IsActive { get; set; }
        public string? Description { get; set; }
    }
}
