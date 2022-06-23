using MeetupTelegramBot.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetupTelegramBot.DataAccess.Configuration
{
    public class MeetupConfiguration : IEntityTypeConfiguration<MeetupEntity>
    {
        public void Configure(EntityTypeBuilder<MeetupEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasMany(src => src.Presentations)
                .WithMany(dest => dest.Meetups)
                .UsingEntity<MeetupPresentationEntity>(
                    j => j.HasOne(mp => mp.Presentation)
                        .WithMany(m => m.MeetupPresentations)
                        .HasForeignKey(mp => mp.PresentationId),
                    j => j.HasOne(mp => mp.Meetup)
                        .WithMany(m => m.MeetupPresentations)
                        .HasForeignKey(mp => mp.MeetupId),
                    j => j.HasKey(p => p.Id)
                    );
        }
    }
}
