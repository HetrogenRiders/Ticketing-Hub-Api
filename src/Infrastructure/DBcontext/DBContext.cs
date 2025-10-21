using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TicketingHub.Api.Common;
using TicketingHub.Api.Common.Interfaces;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Infrastructure;


public class DBContext : DbContext
{
    private readonly ICurrentUserService _currentUserService;
    public DBContext(DbContextOptions<DBContext> options, ICurrentUserService currentUserService) : base(options)
    {
        _currentUserService = currentUserService;

    }
    // 🔹 Master tables
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Project> Projects { get; set; }

    // 🔹 Ticketing
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketComment> TicketComments { get; set; }
    public DbSet<TicketAttachment> TicketAttachments { get; set; }
    public DbSet<TicketHistory> TicketHistories { get; set; }
    public DbSet<TicketSubscriber> TicketSubscribers { get; set; }

    // 🔹 Configuration
    public DbSet<Category> Categories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<Priority> Priorities { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<SLA> SLAs { get; set; }

    // 🔹 Notifications & Customization
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<CustomField> CustomFields { get; set; }
    public DbSet<CustomFieldValue> CustomFieldValues { get; set; }

    // 🔹 System tables
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<SystemSetting> SystemSettings { get; set; }


    //public DbSet<User> User { get; set; }
    //public DbSet<Role> Roles { get; set; }
    //public DbSet<UserRole> UserRoles { get; set; }
    //public DbSet<Domain.Module> Modules { get; set; }
    //public DbSet<Domain.RoleModuleConfiguration> RoleModuleConfiguration { get; set; }
    //public DbSet<RefreshToken> RefreshTokens { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.UserId;
                    entry.Entity.Created = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUserService.UserId;
                    entry.Entity.LastModified = DateTime.Now;
                    break;
                default:
                    break;
            }
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}