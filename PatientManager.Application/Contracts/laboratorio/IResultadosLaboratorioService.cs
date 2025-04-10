using PatientManager.Application.Base;
using PatientManager.Application.Core;
using PatientManager.Application.Dtos.laboratorio;
using PatientManager.Domain.Result;


namespace PatientManager.Application.Contracts.laboratorio
{
    public interface IResultadosLaboratorioService : IBaseService<ServiceResponse, ResultadosLaboratorioDto>
    {
        Task<ServiceResponse> GetResultadosByPatientAsync(int id);

    }
}
