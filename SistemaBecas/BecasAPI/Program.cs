using BecasAPI.Servicios;

var builder = WebApplication.CreateBuilder(args);

// ✅ Agregar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers()
    .AddApplicationPart(typeof(BecasAPI.Controllers.BecasController).Assembly);

builder.Services.AddSingleton<BecaServicio>();
builder.Services.AddSingleton<UsuarioServicio>();
builder.Services.AddSingleton<AlertaService>();

var app = builder.Build();

// ✅ Usar CORS
app.UseCors("AllowAll");

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();