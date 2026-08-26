using AppRoomGameBackend.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//=====================================
Character character = new Character();
character.Gold = 500;
bool result = character.SpendGold(300);

Console.WriteLine($"İşlem Başarılı mı: {result}");
Console.WriteLine($"Geriye Kalan Gold Miktarı:{character.Gold}");
//=====================================www
var app = builder.Build();

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
