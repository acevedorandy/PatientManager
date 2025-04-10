using Azure;
using PatientManager.Application.Base;
using PatientManager.Application.Core;
using PatientManager.Application.Dtos.administracion;
using PatientManager.Application.Dtos.atencionmedica;


namespace PatientManager.Application.Contracts.atencionmedica
{
    public interface IMedicosService : IBaseService<ServiceResponse, MedicosDto>
    {
        Task<ServiceResponse> GetDoctorConvertion(int id);
    }
}
