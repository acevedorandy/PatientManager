using Microsoft.Extensions.DependencyInjection;
using PatientManager.Application.Contracts.laboratorio;
using PatientManager.Application.Services.laboratorio;
using PatientManager.Persistance.Interfaces.laboratorio;
using PatientManager.Persistance.Repositories.laboratorio;
using PatientManager.Persistance.Validations.laboratorio;

namespace PatientManager.IOC.Dependencies.laboratorio
{
    public static class LaboratorioDependency
    {
        public static void AddLaboratorioDependency(this IServiceCollection services)
        {
            // Registro de los repositorios
            services.AddScoped<IPruebasLaboratorioRepository, PruebasLaboratorioRepository>();
            services.AddScoped<IResultadosLaboratorioRepository, ResultadosLaboratorioRepository>();

            // Registro de los servicios
            services.AddTransient<IPruebasLaboratorioService, PruebasLaboratorioService>();
            services.AddTransient<IResultadosLaboratorioService, ResultadosLaboratorioService>();

            // Registro de las validaciones de las entidades
            services.AddScoped<PruebasLaboratorioValidations>();
            services.AddScoped<ResultadosLaboratorioValidations>();
        }
    }
}
