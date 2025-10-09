using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Infrastructure.Configuration
{
    public class PriorityConfiguration : IEntityTypeConfiguration<Priority>
    {
        public void Configure(EntityTypeBuilder<Priority> builder)
        {
            builder.ToTable("Priorities");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.PriorityName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.ColorCode)
                .HasMaxLength(10);

            builder.Property(p => p.SLAHours)
                .HasDefaultValue(0);

            builder.Property(p => p.IsActive)
                .HasDefaultValue(true);
        }
    }
}
