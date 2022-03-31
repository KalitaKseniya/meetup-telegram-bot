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
                    SpeackerName = "Hanna",
                    IsDisplayed = false
                },
                new PresentationDbEntity
                {
                    Id = Guid.NewGuid(),
                    Title = "In-live разработка",
                    Description = "Описание",
                    SpeackerName = "Kseniya",
                    IsDisplayed = false
                },
                new PresentationDbEntity
                {
                    Id = Guid.NewGuid(),
                    Title = "Третий доклад",
                    Description = "Описание",
                    SpeackerName = "Сюрприз",
                    IsDisplayed = false
                },
                new PresentationDbEntity
                {
                    Id = new Guid("fadabc27-40e4-47f3-bc1b-f0916b4772cd"),
                    Title = "Базы данных. То, что вы РЕАЛЬНО будете использовать на проекте",
                    Description = "Описание",
                    SpeackerName = "Hanna",
                    IsDisplayed = true
                },
                new PresentationDbEntity
                {
                    Id = new Guid("0c03ba0b-3b46-42ba-ba39-6b635c9a4bc0"),
                    Title = "REST-архитектура или как усидеть на 6 стулья",
                    Description = "Описание",
                    SpeackerName = "Kseniya",
                    IsDisplayed = true
                },
                new PresentationDbEntity
                {
                    Id = new Guid("99d09f48-0fec-4ef4-8292-2bab81de8d37"),
                    Title = "Реалии фуллстека",
                    Description = "Описание",
                    SpeackerName = "Илья",
                    IsDisplayed = true
                },
                new PresentationDbEntity
                {
                    Id = new Guid("958AE825-56F4-4390-90E3-4AA9741673A3"),
                    Title = "Вопрос не по темам",
                    Description = "Описание",
                    SpeackerName = "",
                    IsDisplayed = true
                }
            );
        }
    }
}