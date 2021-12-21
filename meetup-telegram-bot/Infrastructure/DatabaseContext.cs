using meetup_telegram_bot.Data.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace meetup_telegram_bot.Infrastructure
{
    public class DatabaseContext: DbContext
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Presentation> Presentations { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Presentation>(b => {
                b.HasKey(p => p.Id);
                b.ToTable("Presentations");
            });

            builder.Entity<Feedback>(b =>
            {
                b.HasKey(p => p.Id);
                b.Property(c => c.Date).HasColumnType("date");
                b.Property(c => c.Time).HasColumnType("time");
                b.ToTable("Feedbacks");
            });

            builder.Entity<Question>(b => {
                b.HasKey(c => c.Id);
                b.Property(c => c.PresentationId)
                 .IsRequired();

                b.HasOne<Presentation>()
                    .WithMany()
                    .HasForeignKey(c => c.PresentationId);
                b.Property(b => b.Time).HasColumnType("time");
                b.Property(b => b.Date).HasColumnType("date");
                b.ToTable("Questions");
            });
        }
    }
}
