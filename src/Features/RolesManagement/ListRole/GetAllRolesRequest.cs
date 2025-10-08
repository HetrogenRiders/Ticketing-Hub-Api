using MediatR;
using TicketingHub.Api.Common.Classes;

namespace TicketingHub.Api.Features.RolesManagement
{
    public class GetAllRolesRequest : PageableRequest, IRequest<GetAllRolesResponse>
    {
        public string SortColumn { get; set; } = "RoleName";

    }
}
