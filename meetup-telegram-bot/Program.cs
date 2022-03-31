using meetup_telegram_bot.Infrastructure;
using meetup_telegram_bot.Infrastructure.Interfaces;
using meetup_telegram_bot.Infrastructure.Repositories;
using meetup_telegram_bot.Services;
using meetup_telegram_bot.SignalR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddDbContext<DatabaseContext>(opt =>
                    opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IPresentationRepository, PresentationRepository>();
builder.Services.AddSingleton<INotificationService, NotificationService>();
builder.Services.AddSingleton<ClientStatesService>();

builder.Services.AddSignalR();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseCors(builder => builder
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true)
               .AllowCredentials());


app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/chat");
});

app.Run();
