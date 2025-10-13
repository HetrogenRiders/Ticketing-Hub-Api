using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Features.Admin.Configuration.SubCategoryManagement.UpdateSubCategory
{
    public class UpdateSubCategoryHandler : IRequestHandler<UpdateSubCategoryRequest, UpdateSubCategoryResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateSubCategoryHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<UpdateSubCategoryResponse> Handle(UpdateSubCategoryRequest request, CancellationToken cancellationToken)
        {
            var item = await _context.SubCategories.FirstAsync(p => p.Id == request.SubCategoryId, cancellationToken);

            // update fields
            item.CategoryId = request.CategoryId;
            item.SubCategoryName = request.SubCategoryName?.Trim();
            item.IsActive = request.IsActive;

            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateSubCategoryResponse
            {
                Message = _localizer["SubCategory updated successfully."]
            };
        }
    }
}
