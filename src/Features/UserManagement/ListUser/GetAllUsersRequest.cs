using MediatR;
using TicketingHub.Api.Common.Classes;

namespace TicketingHub.Api.Features.UserManagement
{
    public class GetAllUsersRequest : PageableRequest, IRequest<GetAllUsersResponse>
    {
        public string SortColumn { get; set; } = "FirstName";
    }
}
