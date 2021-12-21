using meetup_telegram_bot.Data.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace meetup_telegram_bot.Infrastructure.Configuration
{
    public class PresentationConfiguration : IEntityTypeConfiguration<PresentationDbEntity>
    {
        public void Configure(EntityTypeBuilder<PresentationDbEntity> builder)
        {
            builder.HasData(
                new PresentationDbEntity
                {
                    Id = Guid.NewGuid(),
                    Title = "Путь разработчика",
                    Description = "Описание",
                    SpeackerName = "Hanna"
                },
                new PresentationDbEntity
                {
                    Id = Guid.NewGuid(),
                    Title = "In-live разработка",
                    Description = "Описание",
                    SpeackerName = "Kseniya"
                },
                new PresentationDbEntity
                {
                    Id = Guid.NewGuid(),
                    Title = "Третий доклад",
                    Description = "Описание",
                    SpeackerName = "Сюрприз"
                }
            );
        }
    }
}