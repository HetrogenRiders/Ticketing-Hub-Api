namespace TicketingHub.Api.Features.UserManagement;
using MediatR;

public class DeleteUserRequest : IRequest<DeleteUserResponse>
{
    public Guid UserId { get; set; }
}