using Microsoft.EntityFrameworkCore;
using RekdApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();
var jwtConfig = new JwtConfig
{
    Issuer = jwtIssuer,
    Key = jwtKey
};
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION") ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = jwtConfig.GetTokenValidationParameters();
 });

builder.Services.AddDbContext<GameDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDbContext<TokenDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddScoped<TokenService>();

builder.Services.AddIdentityCore<User>().AddEntityFrameworkStores<GameDbContext>();

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