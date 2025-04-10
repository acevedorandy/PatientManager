using Microsoft.Extensions.DependencyInjection;
using PatientManager.Application.Contracts.atencionmedica;
using PatientManager.Application.Services.atencionmedica;
using PatientManager.Persistance.Interfaces.atencionmedica;
using PatientManager.Persistance.Repositories.atencionmedica;
using PatientManager.Persistance.Validations.atencionmedica;

namespace PatientManager.IOC.Dependencies.atencionmedica
{
    public static class AtencionMedicaDependency
    {
        public static void AddAtencionMedicaDependency(this IServiceCollection services)
        {
            // Registro de los repositorios
            services.AddScoped<ICitasRepository, CitasRepository>();
            services.AddScoped<IMedicosRepository, MedicosRepository>();
            services.AddScoped<IPacientesRepository, PacientesRepository>();

            // Registro de los servicios
            services.AddTransient<ICitasService, CitasService>();
            services.AddTransient<IMedicosService, MedicosService>();
            services.AddTransient<IPacientesService, PacientesService>();

            // Registro de las validaciones de las entidades
            services.AddScoped<CitasValidations>();
            services.AddScoped<MedicosValidations>();
            services.AddScoped<PacientesValidations>();
        }
    }
}
