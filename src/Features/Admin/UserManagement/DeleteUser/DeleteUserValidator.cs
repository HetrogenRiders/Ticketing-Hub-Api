using FluentValidation;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Features.Admin.DepartmentManagement.UpdateDepartment;
using TicketingHub.Api.Features.Admin.UserManagement.GetUserById;


namespace TicketingHub.Api.Features.Admin.UserManagement.DeleteUser
{
    public class DeleteUserValidator : AbstractValidator<DeleteUserRequest>
    {
        public DeleteUserValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(localizer["Department ID is required."]);
        }
    }
}
