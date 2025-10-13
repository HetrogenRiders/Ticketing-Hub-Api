using MediatR;

namespace TicketingHub.Api.Features.Admin.UserManagement.UpdateUser
{
    public class UpdateUserRequest : IRequest<UpdateUserResponse>
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public Guid DepartmentId { get; set; }
        public Guid RoleId { get; set; }
        public Guid? ManagerId { get; set; }
        public bool IsActive { get; set; }
    }
}
