using Microsoft.EntityFrameworkCore;
using EMailService.Data;
using EMailService.Messaging;
using EMailService.Services;
using EMailService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//service for connection to Database 

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection"));
});

var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection"));
builder.Services.AddSingleton(new EmailService(optionsBuilder.Options));

//
builder.Services.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.useAzure();

app.UseAuthorization();

app.MapControllers();

app.Run();
