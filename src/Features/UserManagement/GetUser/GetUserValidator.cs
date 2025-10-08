using FluentValidation;

namespace TicketingHub.Api.Features.UserManagement
{
    public class GetUserValidator : AbstractValidator<GetUserRequest>
    {
        public GetUserValidator()
        {
            RuleFor(v => v.UserId).NotEmpty();
        }
    }
}
