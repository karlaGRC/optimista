using KGRC_Evaluacion.Interface;
using Microsoft.EntityFrameworkCore;
using KGRC_Evaluacion.Models;
using System.Text;
using KGRC_Evaluacion.Conexion;
using KGRC_Evaluacion.ServiceExtension;
using KGRC_Evaluacion.Controllers;
using KGRC_Evaluacion.Repositories;
using KGRC_Evaluacion.Servicios;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.DataProtection;
using System.IO;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IConfiguration iconfiguration = builder.Configuration;
// Configura el DbContext con SQL Server

builder.Services.AddDIServices(builder.Configuration);
builder.Services.AddDbContext<CoctelDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CoctelConnection")));
builder.Services.Configure<AppSettings>(iconfiguration.GetSection("AppSettings"));
builder.Services.Configure<OptionsSettings>(iconfiguration.GetSection("AppSettings"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<AppSettings>>().Value);
builder.Services.AddSingleton<OptionsSettings>();
// Registra los repositorios

builder.Services.AddScoped<ICoctelRepository, CoctelRepository>();
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddTransient<IRestClient, RestClientRepository>();
builder.Services.AddTransient<IBitacoraRepository, BitacoraRepository>();

// Registra UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddHttpClient();

// Registra el servicio
builder.Services.AddScoped<CoctelServices>();
builder.Services.AddScoped<UsuarioServices>();
builder.Services.AddTransient<RestClientServices>();
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDataProtection()
             .PersistKeysToFileSystem(new DirectoryInfo(Directory.GetCurrentDirectory()));


builder.Services.AddSession(options =>
{
    options.Cookie.Name = "IdUser"; // Nombre de la cookie de sesión
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Tiempo de expiración de la sesión
    options.Cookie.HttpOnly = true; // Cookie accesible solo por HTTP
    options.Cookie.IsEssential = true; // Marcar la cookie como esencial para cumplimiento GDPR
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    // Log the request path
    Console.WriteLine($"Request Path: {context.Request.Path}");
    await next.Invoke();
});


app.UseHttpsRedirection();
app.UseStaticFiles();
// Habilitar el uso de sesiones
app.UseSession();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
