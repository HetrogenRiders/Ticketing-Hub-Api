using FluentValidation;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;


namespace TicketingHub.Api.Features.Admin.DepartmentManagement.CreateDepartment
{
    public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentRequest>
    {
        public CreateDepartmentValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.DepartmentName)
                .NotEmpty().WithMessage(localizer["Department name is required."])
                .MaximumLength(100).WithMessage(localizer["Department name cannot exceed 100 characters."]);
        }
    }
}
