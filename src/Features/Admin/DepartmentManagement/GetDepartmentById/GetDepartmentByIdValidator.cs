using FluentValidation;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;


namespace TicketingHub.Api.Features.Admin.DepartmentManagement.GetDepartmentById
{
    public class GetDepartmentByIdValidator : AbstractValidator<GetDepartmentByIdRequest>
    {
        public GetDepartmentByIdValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage(localizer["Department ID is required."]);
        }
    }
}
