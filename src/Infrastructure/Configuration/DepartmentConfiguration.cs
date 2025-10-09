using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Infrastructure.Configuration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");
            builder.HasKey(d => d.Id);

            builder.Property(d => d.DepartmentName).HasMaxLength(100).IsRequired();

            builder.HasOne(d => d.ParentDepartment)
                .WithMany()
                .HasForeignKey(d => d.ParentDepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.HeadUser)
                .WithMany()
                .HasForeignKey(d => d.HeadUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
