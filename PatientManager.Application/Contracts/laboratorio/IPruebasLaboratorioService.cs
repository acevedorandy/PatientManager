using PatientManager.Application.Base;
using PatientManager.Application.Core;
using PatientManager.Application.Dtos.administracion;
using PatientManager.Application.Dtos.laboratorio;


namespace PatientManager.Application.Contracts.laboratorio
{
    public interface IPruebasLaboratorioService : IBaseService<ServiceResponse, PruebasLaboratorioDto>
    {
    }
}
