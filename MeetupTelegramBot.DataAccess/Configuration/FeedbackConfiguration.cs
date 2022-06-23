using MeetupTelegramBot.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetupTelegramBot.DataAccess.Configuration
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<FeedbackEntity>
    {
        public void Configure(EntityTypeBuilder<FeedbackEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(src => src.Meetup)
                .WithMany(dest => dest.Feedbacks)
                .HasForeignKey(src => src.MeetupId);
        }
    }
}
