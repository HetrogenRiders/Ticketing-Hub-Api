using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Infrastructure.Configuration
{
    public class CustomFieldValueConfiguration : IEntityTypeConfiguration<CustomFieldValue>
    {
        public void Configure(EntityTypeBuilder<CustomFieldValue> builder)
        {
            builder.ToTable("CustomFieldValues");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.FieldValue)
                .HasColumnType("nvarchar(max)");

            builder.HasOne(v => v.CustomField)
                .WithMany(f => f.FieldValues)
                .HasForeignKey(v => v.CustomFieldId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
