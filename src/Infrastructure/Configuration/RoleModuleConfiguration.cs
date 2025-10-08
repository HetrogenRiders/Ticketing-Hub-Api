using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TicketingHub.Api.Infrastructure;

public class RoleModuleConfiguration : IEntityTypeConfiguration<Domain.RoleModuleConfiguration>
{
    public void Configure(EntityTypeBuilder<Domain.RoleModuleConfiguration> builder)
    {
        builder.HasKey(ur => new { ur.RoleID, ur.ModuleID });
    }
}
