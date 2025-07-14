
using Motel.Integracion.Clientes;
using Motel.Integracion.Habitaciones;
using Motel.Integracion.Pagos;
using Motel.Integracion.ReservaHabitacion;
using Motel.Integracion.Reservas;
using Motel.Integracion.ReservaServicios;
using Motel.Integracion.Servicios;
using Motel.Integracion.Usuarios;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<ClienteService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7244/api/");
});

builder.Services.AddHttpClient<HabitacionService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7244/api/");
});

builder.Services.AddHttpClient<PagoService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7244/api/");
});

builder.Services.AddHttpClient<ReservaHabitacionService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7244/api/");
});

builder.Services.AddHttpClient<ReservaService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7244/api/");
});

builder.Services.AddHttpClient<ReservaServiciosService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7244/api/");
});

builder.Services.AddHttpClient<ServicioService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7244/api/");
});
builder.Services.AddHttpClient<UsuarioService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7244/api/"); 
});
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


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
