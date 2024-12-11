using CleanCodeAPI.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>(); // Aspect Oriented Programing Kod blo�unun aras�na girip t�m uygulamadaki kod bloklar�n�n try catch ile sarmalad�k. Hatalar� merkezile�tirdik.

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
