using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingHub.Api.Domain;

namespace TicketingHub.Api.Infrastructure.Configuration
{
    public class TicketSubscriberConfiguration : IEntityTypeConfiguration<TicketSubscriber>
    {
        public void Configure(EntityTypeBuilder<TicketSubscriber> builder)
        {
            builder.ToTable("TicketSubscribers");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Ticket)
                .WithMany(t => t.Subscribers)
                .HasForeignKey(x => x.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.IsFilteringEnabled)
                .HasDefaultValue(false);
        }
    }
}
