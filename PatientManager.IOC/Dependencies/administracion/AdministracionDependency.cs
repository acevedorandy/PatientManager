using Microsoft.Extensions.DependencyInjection;
using PatientManager.Application.Contracts.administracion;
using PatientManager.Application.Services.administracion;
using PatientManager.Persistance.Interfaces.administracion;
using PatientManager.Persistance.Repositories.administracion;
using PatientManager.Persistance.Validations.administracion;

namespace PatientManager.IOC.Dependencies.administracion
{
    public static class AdministracionDependency
    {
        public static void AddAdministracionDependency(this IServiceCollection services)
        {
            // Registro de los repositorios
            services.AddScoped<IConsultoriosRepository, ConsultoriosRepository>();
            services.AddScoped<IEstadosRepository, EstadosRepository>();
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();

            // Registro de los servicios
            services.AddTransient<IConsultoriosServices, ConsultoriosService>();
            services.AddTransient<IEstadosService, EstadosService>();
            services.AddTransient<IUsuariosService, UsuariosService>();

            // Registro de las validaciones de las entidades
            services.AddScoped<ConsultoriosValidations>();
            services.AddScoped<EstadosValidations>();
            services.AddScoped<UsuariosValidations>();

        }
    }
}
