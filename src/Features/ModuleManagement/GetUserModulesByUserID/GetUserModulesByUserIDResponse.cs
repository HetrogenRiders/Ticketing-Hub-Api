namespace TicketingHub.Api.Features.ModuleManagement;
public class GetUserModulesByUserIDResponse
{
    public Guid UserID { get; set; }
    public List<UserModule> Modules { get; set; }
}