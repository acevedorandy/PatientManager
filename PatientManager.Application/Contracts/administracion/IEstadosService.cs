

using PatientManager.Application.Base;
using PatientManager.Application.Core;
using PatientManager.Application.Dtos.administracion;

namespace PatientManager.Application.Contracts.administracion
{
    public interface IEstadosService : IBaseService<ServiceResponse, EstadosDto>
    {
    }
}
