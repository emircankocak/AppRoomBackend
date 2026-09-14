using Microsoft.EntityFrameworkCore;
using AppRoomGameBackend.Data;
using AppRoomGameBackend.Models;

using AppRoomGameBackend.Middleware;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppRoomGameDbContext>(Options =>
Options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//=====================================
Character character = new Character();
character.Gold = 500;
bool result = character.SpendGold(300);

Console.WriteLine($"İşlem Başarılı mı: {result}");
Console.WriteLine($"Geriye Kalan Gold Miktarı:{character.Gold}");
//=====================================
var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
