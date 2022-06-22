using MeetupTelegramBot.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetupTelegramBot.DataAccess.Contexts
{
    public class DatabaseContext: DbContext
    {
        public DbSet<QuestionEntity> Questions { get; set; }
        public DbSet<FeedbackEntity> Feedbacks { get; set; }
        public DbSet<PresentationEntity> Presentations { get; set; }
        public DbSet<MeetupPresentationEntity> MeetupPresentations { get; set; }
        public DbSet<SpeackerEntity> Speackers { get; set; }
        public DbSet<MeetupEntity> Meetups { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}
