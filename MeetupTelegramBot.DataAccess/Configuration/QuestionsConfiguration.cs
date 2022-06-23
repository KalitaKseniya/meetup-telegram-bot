using MeetupTelegramBot.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetupTelegramBot.DataAccess.Configuration
{
    public class QuestionsConfiguration : IEntityTypeConfiguration<QuestionEntity>
    {
        public void Configure(EntityTypeBuilder<QuestionEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(src => src.MeetupPresentation)
                .WithMany(dest => dest.Questions)
                .HasForeignKey(src => src.MeetupPresentationId);
        }
    }
}
