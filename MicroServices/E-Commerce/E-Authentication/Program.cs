using E_Authentication.Data;
using E_Authentication.Models;
using E_Authentication.Services.IServices;
using E_Authentication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using E_Authentication.Utilities;

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

//configure Identity Framework
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//services
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IJwt, JwtService>();

//configure JWTOptions Class
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
