using FluentValidation;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;


namespace TicketingHub.Api.Features.Admin.DepartmentManagement.UpdateDepartment
{
    public class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentRequest>
    {
        public UpdateDepartmentValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage(localizer["Department ID is required."]);

            RuleFor(x => x.DepartmentName)
                .NotEmpty().WithMessage(localizer["Department name is required."])
                .MaximumLength(100).WithMessage(localizer["Department name cannot exceed 100 characters."]);
        }
    }
}
