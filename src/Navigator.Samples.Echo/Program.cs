using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Navigator;
using Navigator.Configuration;
using Navigator.Providers.Telegram;
using Navigator.Samples.Echo.Actions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddNavigator(options =>
{
    options.SetWebHookBaseUrl(builder.Configuration["BASE_WEBHOOK_URL"]);
    options.RegisterActionsFromAssemblies(typeof(EchoAction).Assembly);
}).WithProvider.Telegram(options => { options.SetTelegramToken(builder.Configuration["BOT_TOKEN"]); });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapNavigator()
    .ForProvider.Telegram();

app.Run();