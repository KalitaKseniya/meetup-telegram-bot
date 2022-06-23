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


            //builder.HasData(
            //    new PresentationEntity
            //    {
            //        Id = Guid.NewGuid(),
            //        Title = "Путь разработчика",
            //        Description = "Описание",
            //        //SpeackerId = "Hanna",
            //        IsDisplayed = false
            //    },
            //    new PresentationEntity
            //    {
            //        Id = Guid.NewGuid(),
            //        Title = "In-live разработка",
            //        Description = "Описание",
            //        SpeackerId = "Kseniya",
            //        IsDisplayed = false
            //    },
            //    new PresentationEntity
            //    {
            //        Id = Guid.NewGuid(),
            //        Title = "Третий доклад",
            //        Description = "Описание",
            //        SpeackerId = "Сюрприз",
            //        IsDisplayed = false
            //    },
            //    new PresentationEntity
            //    {
            //        Id = new Guid("fadabc27-40e4-47f3-bc1b-f0916b4772cd"),
            //        Title = "Базы данных. То, что вы РЕАЛЬНО будете использовать на проекте",
            //        Description = "Описание",
            //        SpeackerId = "Hanna",
            //        IsDisplayed = true
            //    },
            //    new PresentationEntity
            //    {
            //        Id = new Guid("0c03ba0b-3b46-42ba-ba39-6b635c9a4bc0"),
            //        Title = "REST-архитектура или как усидеть на 6 стулья",
            //        Description = "Описание",
            //        SpeackerId = "Kseniya",
            //        IsDisplayed = true
            //    },
            //    new PresentationEntity
            //    {
            //        Id = new Guid("99d09f48-0fec-4ef4-8292-2bab81de8d37"),
            //        Title = "Реалии фуллстека",
            //        Description = "Описание",
            //        SpeackerId = "Илья",
            //        IsDisplayed = true
            //    },
            //    new PresentationEntity
            //    {
            //        Id = new Guid("958AE825-56F4-4390-90E3-4AA9741673A3"),
            //        Title = "Вопрос не по темам",
            //        Description = "Описание",
            //        SpeackerId = "",
            //        IsDisplayed = true
            //    }
            //);
        }
    }
}