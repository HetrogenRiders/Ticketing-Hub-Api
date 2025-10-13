using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;

namespace TicketingHub.Api.Features.Admin.Configuration.SubCategoryManagement.GetAllSubCategory
{
    public class GetAllSubCategoryHandler : IRequestHandler<GetAllSubCategoryRequest, GetAllSubCategoryResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public GetAllSubCategoryHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<GetAllSubCategoryResponse> Handle(GetAllSubCategoryRequest request, CancellationToken cancellationToken)
        {
            var items = await _context.SubCategories
                .AsNoTracking()
                .OrderBy(x => x.CategoryId)
                .Select(x => new SubCategoryDto { Id = x.Id, CategoryId = x.CategoryId, IsActive = x.IsActive })
                .ToListAsync(cancellationToken);

            return new GetAllSubCategoryResponse { SubCategoryDtos = items };
        }
    }
}
