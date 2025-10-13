using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Infrastructure;

namespace TicketingHub.Api.Features.Admin.Configuration.SubCategoryManagement.GetByIdSubCategory
{
    public class GetSubCategoryByIdValidator : AbstractValidator<GetSubCategoryByIdRequest>
    {
        private readonly DBContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public GetSubCategoryByIdValidator(DBContext context, IStringLocalizer<SharedResource> localizer)
        {
            _context = context;
            _localizer = localizer;

            RuleFor(x => x.SubCategoryId)
                .NotEmpty().WithMessage(localizer["SubCategory ID is required."])
                .MustAsync(async (id, ct) => await _context.SubCategories.AsNoTracking().AnyAsync(e => e.Id == id, ct))
                .WithMessage(localizer["SubCategory not found."]);
        }
    }
}