var builder = WebApplication.CreateBuilder(args);

// Adiciona apenas o suporte essencial para os Controllers
builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();

// Mapeia os seus controladores (ClientesController e PetsController)
app.MapControllers();

app.Run();
