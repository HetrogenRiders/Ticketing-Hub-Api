using FluentValidation;
using Microsoft.Extensions.Localization;using TicketingHub.Api.Resources;
using TicketingHub.Api.Features.Admin.UserManagement.DeleteUser;


namespace TicketingHub.Api.Features.Admin.RoleManagement.DeleteRole
{
    public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommandRequest>
    {
        public DeleteRoleCommandValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage(localizer["Role ID is required."]);
        }
    }
}
