using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TicketingHub.Api.Domain;
using TicketingHub.Api.Features.UserManagement;
using TicketingHub.Api.UnitTests.Infrastructure;

namespace TicketingHub.Api.UnitTests.Features.UserManagement.DeleteUser;

public class DeleteUserHandlerTests
{

    private DBContextFixture _fixture;
    private DeleteUserHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _fixture = new DBContextFixture();
        _fixture.Initialize();
        _handler = new DeleteUserHandler(_fixture.Context);
    }
    [Test]
    public async Task Handle_UserExists_DeletesUserAndRoles()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User { UserID = userId, IsDeleted = false };
        var userRoles = new[]
        {
            new UserRole { RoleID = Guid.NewGuid(), UserID = userId },
            new UserRole { RoleID = Guid.NewGuid(), UserID = Guid.NewGuid() }
        };

        await _fixture.Context.User.AddAsync(user);
        await _fixture.Context.UserRoles.AddRangeAsync(userRoles);
        await _fixture.Context.SaveChangesAsync();

        var handler = new DeleteUserHandler(_fixture.Context);
        var request = new DeleteUserRequest { UserId = userId };

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.That(response.Success, Is.True);
        Assert.That(response.Message, Is.EqualTo("User deleted successfully."));
        Assert.That((await _fixture.Context.User.FindAsync(userId)).IsDeleted, Is.True);
        Assert.That(_fixture.Context.UserRoles.Where(ur => ur.UserID == userId).Any(), Is.False);
    }

    [Test]
    public async Task Handle_UserDoesNotExist_ReturnsUserNotFoundResponse()
    {
        // Arrange
        var handler = new DeleteUserHandler(_fixture.Context);
        var request = new DeleteUserRequest { UserId = Guid.NewGuid() };

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.That(response.Success, Is.False);
        Assert.That(response.Message, Is.EqualTo("User not found!"));
    }
}