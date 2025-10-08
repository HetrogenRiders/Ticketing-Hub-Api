using FluentValidation;

namespace TicketingHub.Api.Features.RolesManagement.GetRole
{
    public class GetRoleValidator : AbstractValidator<GetRoleRequest>
    {
        public GetRoleValidator()
        {
            RuleFor(v => v.RoleId).NotEmpty();
        }
    }
}
