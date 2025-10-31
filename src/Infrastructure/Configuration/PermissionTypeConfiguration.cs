using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Infrastructure.Configuration
{
    public class PermissionTypeConfiguration : IEntityTypeConfiguration<PermissionType>
    {
        public void Configure(EntityTypeBuilder<PermissionType> builder)
        {
            builder.ToTable("PermissionTypes");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.PermissionName)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(p => p.Description)
                   .HasMaxLength(250);


        }
    }
}
