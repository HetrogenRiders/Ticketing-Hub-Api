using TicketingHub.Api.Domain;
using TicketingHub.Api.Features.UserManagement;
using TicketingHub.Api.UnitTests.Infrastructure;

namespace TicketingHub.Api.UnitTests.Features.UserManagement.GetUser;

[TestFixture]
public class GetUserHandlerTests
{
    private DBContextFixture _fixture;
    private GetUserHandler _handler;
    private Guid _userId;
    private Guid _roleUserId;
    private Guid _deletedUserId;

    [SetUp]
    public void SetUp()
    {
        _fixture = new DBContextFixture();
        _fixture.Initialize();
        SeedTestData();
        _handler = new GetUserHandler(_fixture.Context);
    }

    private void SeedTestData()
    {
        _roleUserId = Guid.NewGuid();
        _userId = Guid.NewGuid();
        _deletedUserId = Guid.NewGuid();

        var roles = new List<Role>
        {
            new Role { Id = Guid.NewGuid(), RoleName = "Admin" },
            new Role { Id = _roleUserId, RoleName = "User" }
        };

        var users = new List<User>
        {
            new User
            {
                UserID = _userId,
                FirstName = "TestUserFirstName",
                LastName = "TestUserLastName",
                EmailId = "Ian.lee@example.com",
                IsDeleted = false
            },
            new User
            {
                UserID = _deletedUserId,
                FirstName = "DeletedUser",
                LastName = "LastName",
                IsDeleted = true
            }
        };

        var userRoles = new List<UserRole>
        {
            new UserRole { UserID = _userId, RoleID = _roleUserId }
        };

        _fixture.Context.AddRange(roles);
        _fixture.Context.AddRange(users);
        _fixture.Context.AddRange(userRoles);
        _fixture.Context.SaveChanges();
    }

    [Test]
    public async Task Handle_ShouldReturnUserWithRoles_WhenUserExists()
    {
        // Arrange
        var request = new GetUserRequest { UserId = _userId };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.User, Is.Not.Null);
            Assert.That(response.User.UserID, Is.EqualTo(_userId));
            Assert.That(response.User.FirstName, Is.EqualTo("TestUserFirstName"));
            Assert.That(response.User.LastName, Is.EqualTo("TestUserLastName"));
            Assert.That(response.User.Roles.ToList(), Has.Count.EqualTo(1));
            Assert.That(response.User.Roles.First().RoleId, Is.EqualTo(_roleUserId));
            Assert.That(response.User.Roles.First().RoleName, Is.EqualTo("User"));
        });
    }

    [Test]
    public async Task Handle_ShouldReturnNull_WhenUserDoesNotExist()
    {
        // Arrange
        var request = new GetUserRequest { UserId = Guid.NewGuid() }; // Non-existent UserID

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.User, Is.Null);
        });
    }

    [Test]
    public async Task Handle_ShouldIgnoreDeletedUsers()
    {        
        var request = new GetUserRequest { UserId = _deletedUserId };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.User, Is.Null);
        });
    }
}
