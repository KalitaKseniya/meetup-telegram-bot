using MeetupTelegramBot.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetupTelegramBot.DataAccess.Configuration
{
    public class PresentationConfiguration : IEntityTypeConfiguration<PresentationEntity>
    {
        public void Configure(EntityTypeBuilder<PresentationEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(src => src.Speacker)
                .WithMany(dest => dest.Presentations)
                .HasForeignKey(src => src.SpeackerId);

            builder.Property(p => p.Title).IsRequired();
        }
    }
}