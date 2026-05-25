using BecasAPI.Servicios;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(BecasAPI.Controllers.BecasController).Assembly);

builder.Services.AddSingleton<BecaServicio>();
builder.Services.AddSingleton<UsuarioServicio>();
builder.Services.AddSingleton<AlertaService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();