using MeetupTelegramBot.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetupTelegramBot.DataAccess.Configuration
{
    public class MeetupPresentationConfiguration : IEntityTypeConfiguration<MeetupPresentationEntity>
    {
        public void Configure(EntityTypeBuilder<MeetupPresentationEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasAlternateKey(x => new { x.MeetupId, x.PresentationId });
        }
    }
}
