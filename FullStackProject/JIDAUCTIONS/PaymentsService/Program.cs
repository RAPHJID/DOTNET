using AuctionMessageBus;
using Microsoft.EntityFrameworkCore;
using PaymentsService.Data;
using PaymentsService.Extensions;
using PaymentsService.Services;
using PaymentsService.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// db con 

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.AddAuth();
builder.AddSwaggenGenExtension();


builder.Services.AddScoped<IBidder, BidderService>();
builder.Services.AddScoped<IPayment, PaymentService>();
builder.Services.AddScoped<IArt, ArtService>();
builder.Services.AddScoped<IMessageBus, MessageBus>();

var app = builder.Build();

Stripe.StripeConfiguration.ApiKey = builder.Configuration.GetValue<string>("Stripe:Key");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMigrations();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
