using MediatR;

namespace TicketingHub.Api.Features.ModuleManagement;

public class GetUserModulesByUserIDRequest : IRequest<GetUserModulesByUserIDResponse>
{
    public Guid UserId { get; set; }
}
public class UserModule
{
    public Guid ModuleId { get; set; }
    public string ModuleName { get; set; }
    public bool? CanView { get; set; }
    public bool? CanAdd { get; set; }
    public bool? CanEdit { get; set; }
    public bool? CanDelete { get; set; }
}