using meetup_telegram_bot.Infrastructure;
using MeetupTelegramBot.BusinessLayer.Configuration;
using MeetupTelegramBot.BusinessLayer.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
ConfigurationManager configuration = builder.Configuration;
builder.Services.ConfigureSqlContext(configuration);
builder.Services.RegisterRepositories();
builder.Services.RegisterServices();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddSignalR();
builder.Services.ConfigureCorsPolicy();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionMiddleware>();
//app.UseHttpsRedirection();

app.UseCors("CorsPolicy");


app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/chat");
});

app.Run();
