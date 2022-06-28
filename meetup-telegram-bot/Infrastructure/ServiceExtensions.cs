using MeetupTelegramBot.BusinessLayer.Interfaces;
using MeetupTelegramBot.BusinessLayer.Services;
using MeetupTelegramBot.DataAccess.Contexts;
using MeetupTelegramBot.DataAccess.Interfaces;
using MeetupTelegramBot.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace meetup_telegram_bot.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<DatabaseContext>(opt =>
                    opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        builder => builder.MigrationsAssembly("MeetupTelegramBot.DataAccess")));

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IPresentationRepository, PresentationRepository>();
            services.AddScoped<ISpeackerRepository, SpeackerRepository>();
            services.AddScoped<IMeetupRepository, MeetupRepository>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IMeetupService, MeetupService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IPresentationService, PresentationService>();
            services.AddSingleton<INotificationService, NotificationService>();
            services.AddSingleton<ClientStatesService>();
        }

        public static void ConfigureCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", builder =>
                    builder.AllowCredentials()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true)
                    );
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services) =>
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Meetup Telegram Bot",
                    Description = "API for meetup bot",
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; 
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile); 
                c.IncludeXmlComments(xmlPath);
                
            });
    }

}
