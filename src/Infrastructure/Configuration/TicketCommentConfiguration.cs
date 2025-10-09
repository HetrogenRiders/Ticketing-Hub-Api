using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Infrastructure.Configuration
{
    public class TicketCommentConfiguration : IEntityTypeConfiguration<TicketComment>
    {
        public void Configure(EntityTypeBuilder<TicketComment> builder)
        {
            builder.ToTable("TicketComments");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CommentText).HasColumnType("nvarchar(max)").IsRequired();

            builder.HasOne(c => c.Ticket)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.CommentedBy)
                .WithMany()
                .HasForeignKey(c => c.CommentedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
