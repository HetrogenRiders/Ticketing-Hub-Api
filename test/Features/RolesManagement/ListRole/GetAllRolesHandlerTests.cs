using TicketingHub.Api.Domain;
using TicketingHub.Api.Features.RolesManagement;
using TicketingHub.Api.UnitTests.Infrastructure;

namespace TicketingHub.Api.UnitTests.Features.RolesManagement.ListRole;

[TestFixture]
internal class GetAllRolesHandlerTests
{
    private DBContextFixture _fixture;
    private GetAllRolesHandler _handler;


    [SetUp]
    public void SetUp()
    {
        _fixture = new DBContextFixture();
        _fixture.Initialize();

        // Seed Roles
        _fixture.Context.Roles.AddRange(
            new Role { Id = Guid.NewGuid(), RoleName = "Admin", Description = "Administrator Role", IsDeleted = false },
            new Role { Id = Guid.NewGuid(), RoleName = "User", Description = "Regular User Role", IsDeleted = false },
            new Role { Id = Guid.NewGuid(), RoleName = "Manager", Description = "Manager Role", IsDeleted = false }
        );

        // Seed RoleModuleConfiguration
        _fixture.Context.RoleModuleConfiguration.AddRange(
            new RoleModuleConfiguration { RoleID = Guid.NewGuid(), ModuleID = Guid.NewGuid(), CanView = true, CanAdd = true, CanEdit = true, CanDelete = true },
            new RoleModuleConfiguration { RoleID = Guid.NewGuid(), ModuleID = Guid.NewGuid(), CanView = true, CanAdd = false, CanEdit = false, CanDelete = false }
        );

        // Seed Modules
        _fixture.Context.Modules.AddRange(
            new Module { Id = Guid.NewGuid(), ModuleName = "Dashboard" },
            new Module { Id = Guid.NewGuid(),ModuleName = "Reports" }
        );

        _fixture.Context.SaveChanges();

        _handler = new GetAllRolesHandler(_fixture.Context);
    }

    [Test]
    public async Task Handle_WithSearchKey_ReturnsFilteredRoles()
    {
        // Arrange      
        var request = new GetAllRolesRequest { SearchKey = "Admin", SortColumn = "RoleName", SortDirection = "Ascending", PageNumber = 1, PageSize = 10 };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.That(response.Roles.ToList(), Has.Count.EqualTo(1));
        Assert.That(response.Roles.First().RoleName, Is.EqualTo("Admin"));
    }
    [Test]
    public async Task Handle_WithPagination_ReturnsPagedRoles()
    {
        // Arrange      
        var request = new GetAllRolesRequest { PageNumber = 1, PageSize = 2 };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.That(response.Roles.ToList(), Has.Count.EqualTo(2)); // Ensure only 2 roles are returned
    }

    [Test]
    public async Task Handle_WithSorting_ReturnsSortedRoles()
    {
        // Arrange
      
        var request = new GetAllRolesRequest { SortColumn = "RoleName", SortDirection = "Descending", PageNumber = 1, PageSize = 10 };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        var roleNames = response.Roles.Select(r => r.RoleName).ToList();
        Assert.That(roleNames, Is.EqualTo(new List<string> { "User", "Manager", "Admin" }));
    }

    [Test]
    public async Task Handle_WithInvalidSearchKey_ReturnsEmptyList()
    {
        // Arrange
       
        var request = new GetAllRolesRequest { SearchKey = "NonExistentKey" };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.That(response.Roles, Is.Empty);
    }
}
