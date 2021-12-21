using meetup_telegram_bot.Data.DbEntities;
using meetup_telegram_bot.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace meetup_telegram_bot.Infrastructure
{
    public class DatabaseContext: DbContext
    {
        public DbSet<QuestionDbEntity> Questions { get; set; }
        public DbSet<FeedbackDbEntity> Feedbacks { get; set; }
        public DbSet<PresentationDbEntity> Presentations { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.ApplyConfiguration(new PresentationConfiguration());
            builder.Entity<PresentationDbEntity>(b => {
                b.HasKey(p => p.Id);
                b.ToTable("Presentations");
            });

            builder.Entity<FeedbackDbEntity>(b =>
            {
                b.HasKey(p => p.Id);
                b.Property(c => c.Date).HasColumnType("date");
                b.Property(c => c.Time).HasColumnType("time");
                b.ToTable("Feedbacks");
            });

            builder.Entity<QuestionDbEntity>(b => {
                b.HasKey(c => c.Id);
                b.Property(c => c.PresentationId);

                b.HasOne<PresentationDbEntity>()
                    .WithMany()
                    .HasForeignKey(c => c.PresentationId);
                b.Property(b => b.Time).HasColumnType("time");
                b.Property(b => b.Date).HasColumnType("date");
                b.ToTable("Questions");
            });
        }
    }
}
