using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Infrastructure.Configuration
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable("Statuses");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.StatusName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(s => s.ColorCode)
                .HasMaxLength(10);

            builder.Property(s => s.SortOrder)
                .HasDefaultValue(0);

            builder.Property(s => s.IsFinalStatus)
                .HasDefaultValue(false);

            builder.Property(s => s.IsActive)
                .HasDefaultValue(true);
        }
    }
}
