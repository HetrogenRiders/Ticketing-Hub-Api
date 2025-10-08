using Microsoft.EntityFrameworkCore;
using TicketingHub.Api.Domain;
using TicketingHub.Api.Infrastructure;

public class DbSeeder
{
    private readonly DBContext _dbContext;

    public DbSeeder(DBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
        // Ensure that the database is created and it's in-memory
        if (_dbContext.Database.IsInMemory())
        {
            if (!_dbContext.User.Any()) // Check if there are any users already
            {
                //adding few user with different roles
                var guestUser = new User
                {
                    EmailId = "guestUser@test.com",
                    FirstName = "Burl",
                    LastName = "Rowland",  // Don't store passwords in plain text in production
                    Password = "h3bxCOJHqx4rMjBCwEnCZkB8gfutQb3h6N/Bu2b9Jn4=", //password is "Test@123"
                    ResetPasswordExpiryTime = null,
                };
                var regularUser = new User
                {
                    EmailId = "regularUser@test.com",
                    FirstName = "Edmond",
                    LastName = "Mckinney",  // Don't store passwords in plain text in production
                    Password = "h3bxCOJHqx4rMjBCwEnCZkB8gfutQb3h6N/Bu2b9Jn4=", //password is "Test@123"
                    ResetPasswordExpiryTime = null,
                };
                var premiumUser = new User
                {
                    EmailId = "premiumUser@test.com",
                    FirstName = "Conrad",
                    LastName = "Boyd",  // Don't store passwords in plain text in production
                    Password = "h3bxCOJHqx4rMjBCwEnCZkB8gfutQb3h6N/Bu2b9Jn4=", //password is "Test@123"
                    ResetPasswordExpiryTime = null,
                };
                var adminUser = new User
                {
                    EmailId = "adminUser@test.com",
                    FirstName = "Alfonzo",
                    LastName = "Vazquez",  // Don't store passwords in plain text in production
                    Password = "h3bxCOJHqx4rMjBCwEnCZkB8gfutQb3h6N/Bu2b9Jn4=", //password is "Test@123"
                    ResetPasswordExpiryTime = null,
                };
                var superAdminUser = new User
                {
                    EmailId = "superAdminUser@test.com",
                    FirstName = "Merrill",
                    LastName = "Horn",  // Don't store passwords in plain text in production
                    Password = "h3bxCOJHqx4rMjBCwEnCZkB8gfutQb3h6N/Bu2b9Jn4=", //password is "Test@123"
                    ResetPasswordExpiryTime = null,
                };

                await _dbContext.User.AddRangeAsync(guestUser, regularUser, premiumUser, superAdminUser, adminUser);
                await _dbContext.SaveChangesAsync();  // Save to generate the UserID

                // Add roles
                var roleGuest = new Role { RoleName = "Guest user", Description = "Can access home page and promotional pages", IsDeleted = false };
                var roleRegularUsers = new Role { RoleName = "Regular Users", Description = "Can interact with the main functionalities and features but might have limited access", IsDeleted = false };
                var rolePremiumUsers = new Role { RoleName = "Premium Users", Description = "Can interact with subscribtion content", IsDeleted = false };
                var roleAdmin = new Role { RoleName = "Administrator", Description = "Can manage site settings and add users", IsDeleted = false };
                var roleSuperAdmin = new Role { RoleName = "SuperAdmin", Description = "Can manage subscription, site settings and add users", IsDeleted = false };
                //var roleUser = new Role { RoleName = "User", Description = "test user", IsDeleted = false };
                //var roleAdmin = new Role { RoleName = "Admin", Description = "test admin", IsDeleted = false };
                //var roleSuperAdmin = new Role { RoleName = "SuperAdmin", Description = "test super admin", IsDeleted = false };
                await _dbContext.Roles.AddRangeAsync(roleGuest, roleRegularUsers, rolePremiumUsers, roleAdmin, roleSuperAdmin);
                await _dbContext.SaveChangesAsync();  // Save roles to generate RoleIDs

                var userWithGuestRole = new UserRole
                {
                    UserID = guestUser.UserID, // Use the generated UserID
                    RoleID = roleGuest.Id // Use the generated RoleID
                };

                var userWithRegularUsersRole = new UserRole
                {
                    UserID = regularUser.UserID, // Use the generated UserID
                    RoleID = roleRegularUsers.Id // Use the generated RoleID
                };
                var userWithPremiumUsersRole = new UserRole
                {
                    UserID = premiumUser.UserID, // Use the generated UserID
                    RoleID = rolePremiumUsers.Id // Use the generated RoleID
                };
                var userWithAdminRole = new UserRole
                {
                    UserID = adminUser.UserID, // Use the generated UserID
                    RoleID = roleAdmin.Id // Use the generated RoleID
                };
                var userWithSuperAdminRole = new UserRole
                {
                    UserID = superAdminUser.UserID, // Use the generated UserID
                    RoleID = roleSuperAdmin.Id // Use the generated RoleID
                };

                //// Assign users to super admin role
                //var userWithSuperAdminRole = new UserRole
                //{
                //    UserID = superAdminUser.UserID, // Use the generated UserID
                //    RoleID = roleSuperAdmin.Id // Use the generated RoleID
                //};

                //var adminuserWithSuperAdminRole = new UserRole
                //{
                //    UserID = adminUser.UserID, // Use the generated UserID
                //    RoleID = roleSuperAdmin.Id // Use the generated RoleID
                //};

                //// Assign user to user role
                //var userWithUserRole = new UserRole
                //{
                //    UserID = user.UserID, // Use the generated UserID
                //    RoleID = roleUser.Id // Use the generated RoleID
                //};
                await _dbContext.UserRoles.AddRangeAsync(userWithAdminRole, userWithSuperAdminRole, userWithGuestRole, userWithPremiumUsersRole, userWithRegularUsersRole);

                await _dbContext.SaveChangesAsync();  // Save the user-role relationships


                // adding new modules
                var usersModule = new Module
                {
                    ModuleName = "User",
                    Description = ""
                };

                var rolesModule = new Module
                {
                    ModuleName = "Role",
                    Description = ""
                };
                await _dbContext.Modules.AddRangeAsync(usersModule, rolesModule);
                await _dbContext.SaveChangesAsync();

                // adding role module configuration
                var userModuleConfigurationUser = new TicketingHub.Api.Domain.RoleModuleConfiguration
                {
                    RoleID = roleGuest.Id,
                    ModuleID = usersModule.Id,
                    CanView = true,
                    CanAdd = false,
                    CanEdit = false,
                    CanDelete = false,
                    IsDeleted = false
                };

                var roleModuleConfigurationUser = new TicketingHub.Api.Domain.RoleModuleConfiguration
                {
                    RoleID = roleRegularUsers.Id,
                    ModuleID = rolesModule.Id,
                    CanView = true,
                    CanAdd = false,
                    CanEdit = false,
                    CanDelete = false,
                    IsDeleted = false
                };


                var userModuleConfigurationAdmin = new TicketingHub.Api.Domain.RoleModuleConfiguration
                {
                    RoleID = rolePremiumUsers.Id,
                    ModuleID = usersModule.Id,
                    CanView = true,
                    CanAdd = false,
                    CanEdit = false,
                    CanDelete = false,
                    IsDeleted = false
                };

                var roleModuleConfigurationAdmin = new TicketingHub.Api.Domain.RoleModuleConfiguration
                {
                    RoleID = roleAdmin.Id,
                    ModuleID = rolesModule.Id,
                    CanView = true,
                    CanAdd = false,
                    CanEdit = false,
                    CanDelete = false,
                    IsDeleted = false
                };

                var roleModuleConfigurationSuperAdmin = new TicketingHub.Api.Domain.RoleModuleConfiguration
                {
                    RoleID = roleSuperAdmin.Id,
                    ModuleID = rolesModule.Id,
                    CanView = true,
                    CanAdd = true,
                    CanEdit = true,
                    CanDelete = true,
                    IsDeleted = true
                };

                var userModuleConfigurationSuperAdmin = new TicketingHub.Api.Domain.RoleModuleConfiguration
                {
                    RoleID = roleSuperAdmin.Id,
                    ModuleID = usersModule.Id,
                    CanView = true,
                    CanAdd = true,
                    CanEdit = true,
                    CanDelete = true,
                    IsDeleted = true
                };
                await _dbContext.RoleModuleConfiguration.AddRangeAsync(roleModuleConfigurationUser, userModuleConfigurationUser, roleModuleConfigurationAdmin, userModuleConfigurationAdmin, roleModuleConfigurationSuperAdmin, userModuleConfigurationSuperAdmin);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
