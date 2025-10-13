using MediatR;

namespace TicketingHub.Api.Features.Admin.Configuration.SubCategoryManagement.DeleteSubCategory
{
    public class DeleteSubCategoryRequest : IRequest<DeleteSubCategoryResponse> 
    {
        public Guid SubCategoryId { get; set; }
    }
}
