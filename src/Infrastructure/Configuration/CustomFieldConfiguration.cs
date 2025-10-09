using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Infrastructure.Configuration
{
    public class CustomFieldConfiguration : IEntityTypeConfiguration<CustomField>
    {
        public void Configure(EntityTypeBuilder<CustomField> builder)
        {
            builder.ToTable("CustomFields");
            builder.HasKey(cf => cf.Id);

            builder.Property(cf => cf.EntityType)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(cf => cf.FieldName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(cf => cf.FieldType)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(cf => cf.SortOrder)
                .HasDefaultValue(0);

            builder.Property(cf => cf.IsRequired)
                .HasDefaultValue(false);

            builder.Property(cf => cf.IsActive)
                .HasDefaultValue(true);
        }
    }
}
