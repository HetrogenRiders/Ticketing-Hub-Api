using TicketingHub.Api.Features.UserManagement;
using TicketingHub.Api.UnitTests.Infrastructure;

namespace TicketingHub.Api.UnitTests.Features.UserManagement.CreateUser;

[TestFixture]
public class CreateUserHandlerTests
{
    private DBContextFixture _fixture;
    private CreateUserHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _fixture = new DBContextFixture();
        _fixture.Initialize();
        _handler = new CreateUserHandler(_fixture.Context);
    }

    [Test]
    public async Task Handle_ShouldCreateUser_WhenValidRequest()
    {
        // Arrange
        var request = new CreateUserRequest
        {
            FirstName = "John",
            LastName = "Doe",
            EmailId = "john.doe@test.com",
            Password = "password123",
            RoleId = new List<Guid> { Guid.NewGuid() }
        };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);
        var savedUser = await _fixture.Context.User.FindAsync(response.UserId);
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.UserId, Is.Not.EqualTo(Guid.Empty)); // Ensure UserID is generated
            Assert.That(response.FirstName, Is.EqualTo(request.FirstName));
            Assert.That(response.LastName, Is.EqualTo(request.LastName));
            Assert.That(response.EmailId, Is.EqualTo(request.EmailId));
            Assert.That(response.IsDeleted, Is.EqualTo(false)); // Default value
            Assert.That(response.RoleId, Is.EqualTo(request.RoleId));

            // Check that the user is saved in the database          
            Assert.That(savedUser, Is.Not.Null);
            Assert.That(savedUser.FirstName, Is.EqualTo(request.FirstName));
            Assert.That(savedUser.LastName, Is.EqualTo(request.LastName));
            Assert.That(savedUser.EmailId, Is.EqualTo(request.EmailId));

            var userRoles = _fixture.Context.UserRoles.Where(ur => ur.UserID == savedUser.UserID).ToList();
            Assert.That(userRoles.Count, Is.EqualTo(1)); // Ensure two roles are assigned
        });
    }

}



