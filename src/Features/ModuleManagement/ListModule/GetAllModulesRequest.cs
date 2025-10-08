using MediatR;
using TicketingHub.Api.Common.Classes;

namespace TicketingHub.Api.Features.ModuleManagement
{
    public class GetAllModulesRequest : PageableRequest, IRequest<GetAllModulesResponse>
    {
        public string SortColumn { get; set; } = "ModuleName";
    }
}
