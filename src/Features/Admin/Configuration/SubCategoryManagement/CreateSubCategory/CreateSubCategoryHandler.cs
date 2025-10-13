using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Features.Admin.Configuration.SubCategoryManagement.CreateSubCategory
{
    public class CreateSubCategoryHandler : IRequestHandler<CreateSubCategoryRequest, CreateSubCategoryResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CreateSubCategoryHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<CreateSubCategoryResponse> Handle(CreateSubCategoryRequest request, CancellationToken cancellationToken)
        {
            bool exists = await _context.SubCategories
                .AnyAsync(p => p.CategoryId == request.CategoryId, cancellationToken);

            if (exists)
                throw new InvalidOperationException(_localizer["SubCategory name already exists."]);

            var item = new SubCategory
            {
                CategoryId = request.CategoryId,
                SubCategoryName = request.SubCategoryName,
                IsActive = request.IsActive,
                Id = Guid.NewGuid()
            };

            _context.SubCategories.Add(item);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateSubCategoryResponse
            {
                SubCategoryId = item.Id,
                Message = _localizer["SubCategory created successfully."]
            };
        }
    }
}
