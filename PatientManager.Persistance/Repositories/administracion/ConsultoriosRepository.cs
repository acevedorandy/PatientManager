
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
    public sealed class ConsultoriosRepository(PatientManagerContext patientManagerContext,
                 ILogger<ConsultoriosRepository> logger, ConsultoriosValidations consultoriosValidations) : BaseRepository<Consultorios>(patientManagerContext), IConsultoriosRepository
    {
        private readonly PatientManagerContext _patientManagerContext = patientManagerContext;
        private readonly ILogger<ConsultoriosRepository> _logger = logger;
        private readonly ConsultoriosValidations _consultoriosValidations = consultoriosValidations;

        public async override Task<OperationResult> Save(Consultorios consultorios)
        { 
            OperationResult result = new OperationResult();

            _consultoriosValidations.ValidateSave(consultorios);

            try
            {
                result = await base.Save(consultorios);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error guardando el consultorio.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> Update(Consultorios consultorios)
        {
            OperationResult result = new OperationResult();

            _consultoriosValidations.ValidateUpdate(consultorios);

            try
            {
                Consultorios? consultoriosToUpdate = await _patientManagerContext.Consultorios.FindAsync(consultorios.ConsultorioID);

                consultoriosToUpdate.ConsultorioID = consultorios.ConsultorioID;
                consultoriosToUpdate.NombreConsultorio = consultorios.NombreConsultorio;

                result = await base.Update(consultoriosToUpdate);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error actualizando el consultorio.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> Remove(Consultorios consultorios)
        {
            OperationResult result = new OperationResult();

            _consultoriosValidations.ValidateRemove(consultorios);

            try
            {
                result = await base.Remove(consultorios);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error eliminando el consultorio.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from administracion in _patientManagerContext.Consultorios
                                     orderby administracion.ConsultorioID descending

                                     select new ConsultoriosModel()
                                     {
                                         ConsultorioID = administracion.ConsultorioID,
                                         NombreConsultorio = administracion.NombreConsultorio

                                     }).AsNoTracking()
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo los consultorios.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> GetById(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from administracion in _patientManagerContext.Consultorios
                                     where administracion.ConsultorioID == id

                                     select new ConsultoriosModel()
                                     {
                                         ConsultorioID = administracion.ConsultorioID,
                                         NombreConsultorio = administracion.NombreConsultorio

                                     }).AsNoTracking()
                                     .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Hubo un error obteniendo el consultorio.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }
    }
}
