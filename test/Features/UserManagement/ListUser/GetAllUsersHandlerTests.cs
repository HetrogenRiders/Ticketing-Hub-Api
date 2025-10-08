using TicketingHub.Api.Domain;
using TicketingHub.Api.Features.UserManagement;
using TicketingHub.Api.UnitTests.Infrastructure;

namespace TicketingHub.Api.UnitTests.Features.UserManagement.GetAllUsers;

[TestFixture]
public class GetAllUsersHandlerTests
{
    private GetAllUsersHandler _handler;
    private DBContextFixture _fixture;

    [SetUp]
    public void SetupFixture()
    {
        _fixture = new DBContextFixture();
        _fixture.Initialize();
        InitializeData();
        _handler = new GetAllUsersHandler(_fixture.Context);
    }

    private void InitializeData()
    {
        Guid roleUserId = Guid.NewGuid();
        Guid roleAdminId = Guid.NewGuid();
        Guid adminId = Guid.NewGuid();
        Guid userId = Guid.NewGuid();
        var users = new List<User>
        {
            new User { UserID = userId,  FirstName = "Ian",LastName = "Lee", EmailId = "Ian.lee@example.com", IsDeleted = false },
            new User { UserID = Guid.NewGuid(), FirstName = "John", LastName = "Doe", EmailId = "john.doe@example.com", IsDeleted = false },
            new User { UserID = adminId , FirstName = "Jane", LastName = "Smith", EmailId = "jane.smith@example.com",IsDeleted = false }
        };

        var roles = new List<Role>
        {
            new Role { Id = roleAdminId, RoleName = "Admin" },
            new Role { Id = roleUserId, RoleName = "User" }
        };

        var userRoles = new List<UserRole>
        {
            new UserRole { UserID = adminId, RoleID = roleAdminId },
            new UserRole { UserID = userId, RoleID = roleUserId }
        };

        _fixture.Context.AddRange(roles);
        _fixture.Context.AddRange(users);
        _fixture.Context.AddRange(userRoles);
        _fixture.Context.SaveChanges();
    }

    [Test]
    public async Task Handle_ReturnsCorrectResults_WhenSearchKeyIsProvided()
    {
        // Arrange
        var request = new GetAllUsersRequest
        {
            SearchKey = "John",
            SortColumn = "FirstName",
            SortDirection = "Ascending",
            PageNumber = 1,
            PageSize = 10
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.Users.Count, Is.EqualTo(1));
            Assert.That(result.Users.ToList()[0].FirstName, Is.EqualTo("John"));
        });

    }

    [Test]
    public async Task Handle_WhenSearchKeyIsNullOrEmpty_ReturnsAllUsers()
    {

        var request = new GetAllUsersRequest { SearchKey = null };
        // Act
        var result = await _handler.Handle(request, CancellationToken.None);
        Assert.That(result.Users.Count, Is.EqualTo(3));
    }

    [Test]
    public async Task Handle_ShouldReturnUsers_WhenSearchKeyIsEmpty()
    {
        var request = new GetAllUsersRequest
        {
            SearchKey = string.Empty,
            SortColumn = "FirstName",
            SortDirection = "Ascending",
            PageNumber = 1,
            PageSize = 10
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Users.Count, Is.EqualTo(3));
            Assert.That(result.Users.ToList()[0].FirstName, Is.EqualTo("Ian"));
            Assert.That(result.Users.First().Roles.First().RoleName, Is.EqualTo("User"));
        });
    }

    [Test]
    public async Task Handle_ShouldReturnEmptyList_WhenNoUsersMatchSearchKey()
    {
        var request = new GetAllUsersRequest
        {
            SearchKey = "NonExistentName",
            SortColumn = "FirstName",
            SortDirection = "Ascending",
            PageNumber = 1,
            PageSize = 10
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Users.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task Handle_ShouldReturnCorrectUsers_WhenPaginated()
    {

        var request = new GetAllUsersRequest
        {
            SearchKey = string.Empty,
            SortColumn = "FirstName",
            SortDirection = "Ascending",
            PageNumber = 1,
            PageSize = 2
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Users.Count, Is.EqualTo(2)); //Verify pagination
            Assert.That(result.Users.ToList()[1].FirstName, Is.EqualTo("Jane"));// // Verify second user in sorted order   
        });
    }

    [Test]
    public async Task Handle_ShouldSortUsersByLastNameDesc()
    {
        var request = new GetAllUsersRequest
        {
            SearchKey = string.Empty,
            SortColumn = "LastName",
            SortDirection = "Descending",
            PageNumber = 1,
            PageSize = 10
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Users.Count, Is.EqualTo(3)); //Verify pagination
            Assert.That(result.Users.ToList()[1].LastName, Is.EqualTo("Lee"));// Verify descending order
        });

    }

    [Test]
    public async Task Handle_ShouldFilterUsersByEmail()
    {
        var request = new GetAllUsersRequest
        {
            SearchKey = "john.doe@example.com",
            SortColumn = "FirstName",
            SortDirection = "Ascending",
            PageNumber = 1,
            PageSize = 10
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Users.Count, Is.EqualTo(1));
            Assert.That(result.Users.ToList()[0].FirstName, Is.EqualTo("John"));
        });
    }
}

