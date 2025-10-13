using FluentValidation;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;


namespace TicketingHub.Api.Features.Admin.UserManagement.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.EmployeeCode)
                .NotEmpty().WithMessage(localizer["Employee code is required."])
                .MaximumLength(50).WithMessage(localizer["Employee code cannot exceed 50 characters."]);

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage(localizer["Full name is required."])
                .MaximumLength(150).WithMessage(localizer["Full name cannot exceed 150 characters."]);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(localizer["Email is required."])
                .EmailAddress().WithMessage(localizer["Invalid email format."])
                .MaximumLength(150).WithMessage(localizer["Email cannot exceed 150 characters."]);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(localizer["Password is required."])
                .MinimumLength(6).WithMessage(localizer["Password must be at least 6 characters."]);

            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage(localizer["Department is required."]);

            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage(localizer["Role is required."]);
        }
    }
}
