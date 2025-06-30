var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllers();

// Configuraci�n de Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar el pipeline de solicitud HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Habilita Swagger para generar la especificaci�n de la API
    app.UseSwaggerUI(); // Habilita Swagger UI para interactuar con la API desde el navegador
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
