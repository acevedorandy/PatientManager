

using PatientManager.Domain.Entities.laboratorio;
using PatientManager.Domain.Repositories;
using PatientManager.Domain.Result;

namespace PatientManager.Persistance.Interfaces.laboratorio
{
    public interface IResultadosLaboratorioRepository : IBaseRepository<ResultadosLaboratorio>
    {
        Task<OperationResult> GetResultadosByPatient(int id);
    }
}
