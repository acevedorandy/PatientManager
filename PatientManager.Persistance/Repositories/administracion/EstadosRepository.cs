

using PatientManager.Domain.Entities.administracion;
using PatientManager.Domain.Result;
using PatientManager.Persistance.Base;
using PatientManager.Persistance.Context;
using PatientManager.Persistance.Interfaces.administracion;
using PatientManager.Persistance.Models.administracion;
using PatientManager.Persistance.Validations.administracion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PatientManager.Persistance.Repositories.administracion
{
    public sealed class EstadosRepository(PatientManagerContext patientManagerContext,
                 ILogger<EstadosRepository> logger, EstadosValidations estadosValidations) : BaseRepository<Estados>(patientManagerContext), IEstadosRepository
    {
        private readonly PatientManagerContext _patientManagerContext = patientManagerContext;
        private readonly ILogger<EstadosRepository> _logger = logger;
        private readonly EstadosValidations _estadosValidations = estadosValidations;

       public async override Task<OperationResult> Save(Estados estados)
        {
            OperationResult result = new OperationResult();

            _estadosValidations.ValidateSave(estados);

            try
            {
                result = await base.Save(estados);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error guardando el estado.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> Update(Estados estados)
        {
            OperationResult result = new OperationResult();

            _estadosValidations.ValidateUpdate(estados);

            try
            {
                Estados? estadosToUpdate = await _patientManagerContext.Estados.FindAsync(estados.EstadoID);

                estadosToUpdate.EstadoID = estados.EstadoID;
                estadosToUpdate.Nombre = estados.Nombre;

                result = await base.Update(estadosToUpdate);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Hubo un error actualizando el estado.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> Remove(Estados estados)
        {
            OperationResult result = new OperationResult();

            _estadosValidations.ValidateRemove(estados);

            try
            {
                result = await base.Remove(estados);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Hubo un error eliminando el estado.";
                _logger.LogError (result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from administracion in _patientManagerContext.Estados
                                     orderby administracion.EstadoID descending
                                     
                                     select new EstadosModel()
                                     { EstadoID = administracion.EstadoID,
                                       Nombre = administracion.Nombre

                                     }).AsNoTracking()
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Hubo un error obteniendo los estados.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> GetById(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from administracion in _patientManagerContext.Estados
                                     where administracion.EstadoID == id

                                     select new EstadosModel()
                                     {
                                         EstadoID = administracion.EstadoID,
                                         Nombre = administracion.Nombre

                                     }).AsNoTracking()
                                     .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Hubo un error obteniendo el estado.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }
    }
}
