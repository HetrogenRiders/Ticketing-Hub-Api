using FluentValidation;

namespace TicketingHub.Api.Features.ModuleManagement;
public class GetUserModulesByUserIDValidator : AbstractValidator<GetUserModulesByUserIDRequest>
{
    public GetUserModulesByUserIDValidator()
    {
        RuleFor(v => v.UserId).NotEmpty();
    }
}
