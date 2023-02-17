using Microsoft.EntityFrameworkCore;
using FlightControlWebAPI.DAL;
using FlightControlWebAPI.Services;
using FlightControlWebAPI.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("Connect")));

builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<ILoggerService, LoggerService>();
builder.Services.AddScoped<ITerminalService, TerminalService>();
builder.Services.AddHostedService<ControlBackgroundService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
