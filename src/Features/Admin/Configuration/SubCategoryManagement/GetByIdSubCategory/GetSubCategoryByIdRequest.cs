using MediatR;

namespace TicketingHub.Api.Features.Admin.Configuration.SubCategoryManagement.GetByIdSubCategory
{
    public class GetSubCategoryByIdRequest : IRequest<GetSubCategoryByIdResponse> 
    {
        public Guid SubCategoryId { get; set; }
    }
}
