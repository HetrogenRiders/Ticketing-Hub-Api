using FluentValidation;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;


namespace TicketingHub.Api.Features.Admin.UserManagement.UpdateUser
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(localizer["User ID is required."]);

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage(localizer["Full name is required."])
                .MaximumLength(150).WithMessage(localizer["Full name cannot exceed 150 characters."]);

            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage(localizer["Department is required."]);

            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage(localizer["Role is required."]);
        }
    }
}
