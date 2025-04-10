using PatientManager.Application.Base;
using PatientManager.Application.Core;
using PatientManager.Application.Dtos.atencionmedica;


namespace PatientManager.Application.Contracts.atencionmedica
{
    public interface IPacientesService : IBaseService<ServiceResponse, PacientesDto>
    {
        Task<ServiceResponse> GetPacienteConvertion(int id);

    }
}
