using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Features.Admin.Configuration.SubCategoryManagement.GetByIdSubCategory
{
    public class GetSubCategoryByIdHandler : IRequestHandler<GetSubCategoryByIdRequest, GetSubCategoryByIdResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public GetSubCategoryByIdHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<GetSubCategoryByIdResponse> Handle(GetSubCategoryByIdRequest request, CancellationToken cancellationToken)
        {
            var item = await _context.SubCategories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.SubCategoryId, cancellationToken);

            if (item == null)
                throw new KeyNotFoundException(_localizer["SubCategory not found."]);

            return new GetSubCategoryByIdResponse
            {
                CategoryId = item.CategoryId,
                SubCategoryName = item.SubCategoryName,
                IsActive = item.IsActive,
            };
        }
    }
}
