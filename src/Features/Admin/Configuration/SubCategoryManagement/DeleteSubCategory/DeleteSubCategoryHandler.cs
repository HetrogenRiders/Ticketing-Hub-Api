using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Features.Admin.Configuration.SubCategoryManagement.DeleteSubCategory
{
    public class DeleteSubCategoryHandler : IRequestHandler<DeleteSubCategoryRequest, DeleteSubCategoryResponse>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteSubCategoryHandler(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<DeleteSubCategoryResponse> Handle(DeleteSubCategoryRequest request, CancellationToken cancellationToken)
        {
            var item = await _context.SubCategories.FirstAsync(p => p.Id == request.SubCategoryId, cancellationToken);

            _context.SubCategories.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);

            return new DeleteSubCategoryResponse { Message = _localizer["SubCategory deleted successfully."] };
        }
    }
}
