using FluentValidation;

namespace TicketingHub.Api.Features.UserManagement
{
    public class GetAllUsersValidator : AbstractValidator<GetAllUsersRequest>
    {
        public GetAllUsersValidator()
        {
        }
    }
}
