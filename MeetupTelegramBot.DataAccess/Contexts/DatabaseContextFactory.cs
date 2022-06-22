using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MeetupTelegramBot.DataAccess.Contexts
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        DatabaseContext IDesignTimeDbContextFactory<DatabaseContext>.CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
