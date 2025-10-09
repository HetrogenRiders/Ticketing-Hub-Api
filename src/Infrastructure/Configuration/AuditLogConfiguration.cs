using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Infrastructure.Configuration
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.EntityName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.Action)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(a => a.OldValue)
                .HasColumnType("nvarchar(max)");

            builder.Property(a => a.NewValue)
                .HasColumnType("nvarchar(max)");

            builder.HasOne(a => a.PerformedBy)
                .WithMany()
                .HasForeignKey(a => a.PerformedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
