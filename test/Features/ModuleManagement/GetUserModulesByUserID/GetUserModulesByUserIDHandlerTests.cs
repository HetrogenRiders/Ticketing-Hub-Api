using Microsoft.EntityFrameworkCore;
using TicketingHub.Api.Domain;
using TicketingHub.Api.Features.ModuleManagement;
using TicketingHub.Api.UnitTests.Infrastructure;

namespace TicketingHub.Api.UnitTests.Features.ModuleManagement.GetUserModulesByUserID;

[TestFixture]
internal class GetUserModulesByUserIDHandlerTests
{
    private DBContextFixture _fixture;
    private GetUserModulesByUserIDHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _fixture = new DBContextFixture();
        _fixture.Initialize();

        // Seed data
        SeedData();

        // Initialize handler
        _handler = new GetUserModulesByUserIDHandler(_fixture.Context);
    }
    private void SeedData()
    {
        // Seed Roles
        var roles = new[]
        {
            new Role { Id = Guid.NewGuid(), RoleName = "Admin", IsDeleted = false },
            new Role { Id = Guid.NewGuid(), RoleName = "User", IsDeleted = false },
        };

        _fixture.Context.Roles.AddRange(roles);

        // Seed UserRoles
        var userId = Guid.NewGuid();
        var userRoles = new[]
        {
            new UserRole { UserID = userId, RoleID = roles[0].Id }, // Admin Role
            new UserRole { UserID = userId, RoleID = roles[1].Id }, // User Role
        };

        _fixture.Context.Roles.AddRange(userRoles);

        // Seed Modules
        var modules = new[]
        {
            new Module { Id = Guid.NewGuid(), ModuleName = "Dashboard" },
            new Module { Id = Guid.NewGuid(), ModuleName = "Reports" },
        };

        _fixture.Context.Modules.AddRange(modules);

        // Seed RoleModuleConfiguration
        var roleModuleConfigurations = new[]
        {
            new RoleModuleConfiguration
            {
                RoleID = roles[0].Id, ModuleID = modules[0].Id, CanView = true, CanAdd = true, CanEdit = true, CanDelete = true
            },
            new RoleModuleConfiguration
            {
                RoleID = roles[1].Id, ModuleID = modules[1].Id, CanView = true, CanAdd = false, CanEdit = false, CanDelete = false
            },
        };

        _fixture.Context.RoleModuleConfiguration.AddRange(roleModuleConfigurations);

        _fixture.Context.SaveChanges();
    }

    [Test]
    public async Task Handle_ShouldReturnCorrectModulesForUser()
    {
        // Arrange
        var userId = _fixture.Context.UserRoles.First().UserID;
        var request = new GetUserModulesByUserIDRequest { UserId = userId };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.UserID, Is.EqualTo(userId));
        Assert.That(response.Modules, Has.Count.EqualTo(2));

        // Verify modules
        var dashboardModule = response.Modules.FirstOrDefault(m => m.ModuleName == "Dashboard");
        Assert.That(dashboardModule, Is.Not.Null);
        Assert.That(dashboardModule.CanView, Is.True);
        Assert.That(dashboardModule.CanAdd, Is.True);
        Assert.That(dashboardModule.CanEdit, Is.True);
        Assert.That(dashboardModule.CanDelete, Is.True);

        var reportsModule = response.Modules.FirstOrDefault(m => m.ModuleName == "Reports");
        Assert.That(reportsModule, Is.Not.Null);
        Assert.That(reportsModule.CanView, Is.True);
        Assert.That(reportsModule.CanAdd, Is.False);
        Assert.That(reportsModule.CanEdit, Is.False);
        Assert.That(reportsModule.CanDelete, Is.False);
    }
    [Test]
    public async Task Handle_ShouldReturnEmptyModules_WhenUserHasNoRoles()
    {
        // Arrange
        var userId = Guid.NewGuid(); // User with no roles
        var request = new GetUserModulesByUserIDRequest { UserId = userId };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.UserID, Is.EqualTo(userId));
        Assert.That(response.Modules, Is.Empty);
    }

    [Test]
    public async Task Handle_ShouldReturnEmptyModules_WhenUserRolesHaveNoModuleConfigurations()
    {
        // Arrange
        // Add a new role and associate it with a new user, but no role-module configurations
        var userId = Guid.NewGuid();
        var roleId = Guid.NewGuid();
        var role = new Role { Id = roleId, RoleName = "Guest", IsDeleted = false };
        var userRole = new UserRole { UserID = userId, RoleID = roleId };

        _fixture.Context.Roles.Add(role);
        _fixture.Context.UserRoles.Add(userRole);
        await _fixture.Context.SaveChangesAsync();

        var request = new GetUserModulesByUserIDRequest { UserId = userId };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.UserID, Is.EqualTo(userId));
        Assert.That(response.Modules, Is.Empty);
    }

}
