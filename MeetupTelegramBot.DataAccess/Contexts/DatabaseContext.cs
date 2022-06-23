using MeetupTelegramBot.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            Seeding(builder);
        }
        
        private void Seeding(ModelBuilder builder)
        {
            var meetup = new MeetupEntity
            {
                Id = new Guid("7EF9EADE-92A2-4277-94DF-45B802157EF3"),
                Date = new DateTime(2022, 06, 23),
                Place = "Polotsk",
                Time = new TimeSpan(18, 30, 0),
            };
            builder.Entity<MeetupEntity>().HasData(meetup);

            var speacker = new SpeackerEntity
            {
                Id = new Guid("2EF9EADE-92A2-4277-94DF-45B802157EF3"),
                FirstName = "Ivan",
                LastName = "Ivanov",
            };
            builder.Entity<SpeackerEntity>().HasData(speacker);
            
            var feedback = new FeedbackEntity
            {
                Id = new Guid("1EF9EADE-92A2-4277-94DF-45B802157EF3"),
                FutureProposal = "Future proposal",
                GeneralFeedback = "General Feedback",
                AuthorName = "Author Name",
                Time = new TimeSpan(18, 40, 0),
                MeetupId = meetup.Id,
            };
            builder.Entity<FeedbackEntity>().HasData(feedback);

            var presentation = new PresentationEntity
            {
                Id = new Guid("9EF9EADE-92A2-4277-94DF-45B802157EF3"),
                Description = "Description",
                SpeackerId = speacker.Id
            };
            builder.Entity<PresentationEntity>().HasData(presentation);
            
            var meetupPresentation = new MeetupPresentationEntity
            {
                Id = new Guid("6EF9EADE-92A2-4277-94DF-45B802157EF3"),
                MeetupId = meetup.Id,
                PresentationId = presentation.Id
            };
            builder.Entity<MeetupPresentationEntity>().HasData(meetupPresentation);

            var question = new QuestionEntity
            {
                Id = new Guid("8EF9EADE-92A2-4277-94DF-45B802157EF3"),
                Time = new TimeSpan(18, 40, 0),
                Text = "Sample text",
                AuthorName = "Author",
                MeetupPresentationId = meetupPresentation.Id
            };
            builder.Entity<QuestionEntity>().HasData(question);
        }
    }
}
