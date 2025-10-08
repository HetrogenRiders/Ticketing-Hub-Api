using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TicketingHub.Api.Domain;
using TicketingHub.Api.Features.UserManagement;
using TicketingHub.Api.UnitTests.Infrastructure;

namespace TicketingHub.Api.UnitTests.Features.UserManagement.EditUser;

public class EditUserHandlerTests
{

    private DBContextFixture _fixture;
    private EditUserHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _fixture = new DBContextFixture();
        _fixture.Initialize();
        _handler = new EditUserHandler(_fixture.Context);
    }

    [Test]
    public async Task Handle_UserExists_UpdatesUserAndRoles()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var roleId= Guid.NewGuid();
        var user = new User { UserID = userId, FirstName = "John", LastName = "Doe", EmailId = "john.doe@example.com", Password = "OldPasswordHash" };
        var userRoles = new[]
        {
            new UserRole { RoleID = Guid.NewGuid(), UserID = userId },
            new UserRole { RoleID = Guid.NewGuid(), UserID = Guid.NewGuid() }
        };

        await _fixture.Context.User.AddAsync(user);
        await _fixture.Context.UserRoles.AddRangeAsync(userRoles);
        await _fixture.Context.SaveChangesAsync();

        var handler = new EditUserHandler(_fixture.Context);
        var request = new EditUserRequest
        {
            UserId = userId,
            FirstName = "Jane",
            LastName = "Smith",
            EmailId = "jane.smith@example.com",
            Password = "NewPassword123",
            RoleId = new List<Guid> { roleId } // New roles
        };

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        var updatedUser = await _fixture.Context.User.FindAsync(userId);
        var updatedRoles = await _fixture.Context.UserRoles.Where(ur => ur.UserID == userId).ToListAsync();

        Assert.That(updatedUser.FirstName, Is.EqualTo("Jane"));
        Assert.That(updatedUser.LastName, Is.EqualTo("Smith"));
        Assert.That(updatedUser.EmailId, Is.EqualTo("jane.smith@example.com"));

        // Verify password is hashed
        using (var sha256 = SHA256.Create())
        {
            var hashedPassword = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes("NewPassword123")));
            Assert.That(updatedUser.Password, Is.EqualTo(hashedPassword));
        }

        // Verify roles are updated
        Assert.That(updatedRoles.Select(ur => ur.RoleID), Is.EquivalentTo(new List<Guid> { roleId }));

        // Verify response
        Assert.That(response.UserId, Is.EqualTo(userId));
        Assert.That(response.FirstName, Is.EqualTo("Jane"));
        Assert.That(response.LastName, Is.EqualTo("Smith"));
        Assert.That(response.EmailId, Is.EqualTo("jane.smith@example.com"));
        Assert.That(response.RoleId, Is.EquivalentTo(new List<Guid> { roleId }));
    }

    [Test]
    public void Handle_UserDoesNotExist_ThrowsException()
    {
        // Arrange
        var handler = new EditUserHandler(_fixture.Context);
        var request = new EditUserRequest
        {
            UserId = Guid.NewGuid(), // Non-existent user
            FirstName = "Test",
            LastName = "User",
            EmailId = "test.user@example.com",
            Password = "TestPassword",
            RoleId = new List<Guid> { Guid.NewGuid() }
        };

        // Act & Assert
        Assert.ThrowsAsync<Exception>(async () =>
        {
            await handler.Handle(request, CancellationToken.None);
        }, "User not found!");
    }

}