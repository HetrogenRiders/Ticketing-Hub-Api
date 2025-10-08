using FluentValidation;

namespace TicketingHub.Api.Features.RolesManagement;

public class EditRoleValidator : AbstractValidator<EditRoleRequest>
{
    public EditRoleValidator()
    {
        RuleFor(v => v.RoleId).NotEmpty();

        RuleFor(v => v.RoleName).NotEmpty();
    }
}