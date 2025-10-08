using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Features.ModuleManagement
{
    public class GetAllModulesResponse
    {
        public IEnumerable<Module> Modules { get; set; }
    }
}
