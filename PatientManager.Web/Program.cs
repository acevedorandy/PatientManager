using Microsoft.EntityFrameworkCore;
using PatientManager.IOC.Dependencies.administracion;
using PatientManager.IOC.Dependencies.atencionmedica;
using PatientManager.IOC.Dependencies.laboratorio;
using PatientManager.Persistance.Context;
using PatientManager.Web.Controllers.HelperController;
using PatientManager.Web.HelpersWeb.administracion;
using PatientManager.Web.HelpersWeb.administracion.usuario;
using PatientManager.Web.HelpersWeb.atencionmedica;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PatientManagerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PatientManager")));

// Registro de las dependencia del esquema administracion
builder.Services.AddAdministracionDependency();

// Registro de las dependencia del esquema atencionmedica
builder.Services.AddAtencionMedicaDependency();

// Registro de las dependencia del esquema laboratorio
builder.Services.AddLaboratorioDependency();

// Registro de las dependencias de la web
builder.Services.AddTransient<RegisterHelper, RegisterHelper>();
builder.Services.AddTransient<ValidateUserSesion, ValidateUserSesion>();
builder.Services.AddTransient<UsuarioHelper, UsuarioHelper>();
builder.Services.AddTransient<MedicoHelper,  MedicoHelper>();
builder.Services.AddTransient<PacienteHelper, PacienteHelper>();
builder.Services.AddTransient<EstadoHelper, EstadoHelper>();

// Registro para el uso de las sesiones
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Registro del controller ValidationUserController
builder.Services.AddControllersWithViews(option =>
{
    option.Filters.Add<ValidateUserController>();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
