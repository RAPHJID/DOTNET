using EMailService.Extensions;
using Microsoft.EntityFrameworkCore;
using RewardsService.Data;
using RewardsService.Extensions;
using RewardsService.Messaging;
using RewardsService.Service;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.AddAuth();
builder.AddSwaggenGenExtension();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection"));
});

var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection"));
builder.Services.AddSingleton(new RewardService(optionsBuilder.Options));


builder.Services.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumer>();

//register automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMigrations();

app.useAzure();

app.UseAuthorization();

app.MapControllers();

app.Run();