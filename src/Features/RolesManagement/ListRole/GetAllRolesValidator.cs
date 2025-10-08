using FluentValidation;

namespace TicketingHub.Api.Features.RolesManagement.ListRole
{
    public class GetAllRolesValidator : AbstractValidator<GetAllRolesRequest>
    {
        public GetAllRolesValidator()
        {
        }
    }
}
