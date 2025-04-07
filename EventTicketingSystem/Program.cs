using System.IO;
using EventTicketingSystem.Infrastructure;
using EventTicketingSystem.Repositories;
using EventTicketingSystem.Repositories.Interfaces;
using EventTicketingSystem.Services;
using EventTicketingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// This is added so that my frontend can communicate without having cors errors when calling the apis.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Database connection
var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "events_database.db");
var connectionString = $"Data Source={dbPath};Version=3;";

// Register repositories and services
builder.Services.AddSingleton<IEventRepository>(provider => new EventRepository(connectionString));
builder.Services.AddSingleton<ITicketSaleRepository>(provider => new TicketSaleRepository(connectionString));
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<ITicketService, TicketService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();