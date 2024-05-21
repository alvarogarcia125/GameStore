using ApplicationCore.Interfaces.Context;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Mappers;
using ApplicationCore.Services;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

builder.Services.AddAutoMapper(typeof(GameProfile));
builder.Services.AddAutoMapper(typeof(GenreProfile));
builder.Services.AddAutoMapper(typeof(PlatformProfile));

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));

builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IGameRepository, GameRepository>();

builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();

builder.Services.AddScoped<IPlatformService, PlatformService>();
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();

builder.Services.AddScoped<IGameGenreService, GameGenreService>();
builder.Services.AddScoped<IGameGenreRepository, GameGenreRepository>();

builder.Services.AddScoped<IGamePlatformService, GamePlatformService>();
builder.Services.AddScoped<IGamePlatformRepository, GamePlatformRepository>();

builder.Services.AddDbContext<DatabaseContext>(options =>
                    options.UseSqlServer("Data Source=ALVARO-PC\\SQLEXPRESS;Initial Catalog=GameStorage;Integrated Security=True;TrustServerCertificate=True;"));


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
