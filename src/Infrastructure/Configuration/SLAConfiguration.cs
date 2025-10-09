using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Infrastructure.Configuration
{
    public class SLAConfiguration : IEntityTypeConfiguration<SLA>
    {
        public void Configure(EntityTypeBuilder<SLA> builder)
        {
            builder.ToTable("SLA");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.SLAName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(s => s.ResponseTimeInHours)
                .IsRequired();

            builder.Property(s => s.ResolutionTimeInHours)
                .IsRequired();

            builder.Property(s => s.EscalationLevel1Hours);
            builder.Property(s => s.EscalationLevel2Hours);

            builder.Property(s => s.IsActive)
                .HasDefaultValue(true);
        }
    }
}
