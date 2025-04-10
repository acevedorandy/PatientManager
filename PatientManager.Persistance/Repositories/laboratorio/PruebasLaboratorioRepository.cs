
using PatientManager.Domain.Entities.laboratorio;
using PatientManager.Domain.Result;
using PatientManager.Persistance.Base;
using PatientManager.Persistance.Context;
using PatientManager.Persistance.Interfaces.laboratorio;
using PatientManager.Persistance.Models.laboratorio;
using PatientManager.Persistance.Validations.laboratorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PatientManager.Persistance.Repositories.laboratorio
{
    public sealed class PruebasLaboratorioRepository(PatientManagerContext patientManagerContext,
                 ILogger<PruebasLaboratorioRepository> logger, PruebasLaboratorioValidations pruebasLaboratorioValidations) : BaseRepository<PruebasLaboratorio>(patientManagerContext), IPruebasLaboratorioRepository
    {
        private readonly PatientManagerContext _patientManagerContext = patientManagerContext;
        private readonly ILogger<PruebasLaboratorioRepository> _logger = logger;
        private readonly PruebasLaboratorioValidations _pruebasLaboratorioValidations = pruebasLaboratorioValidations;

        public async override Task<OperationResult> Save(PruebasLaboratorio pruebasLaboratorio)
        {
            OperationResult result = new OperationResult();

            _pruebasLaboratorioValidations.ValidateSave(pruebasLaboratorio);

            try
            {
                result = await base.Save(pruebasLaboratorio);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error guardando la prueba";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> Update(PruebasLaboratorio pruebasLaboratorio)
        {
            OperationResult result = new OperationResult();

            _pruebasLaboratorioValidations.ValidateUpdate(pruebasLaboratorio);

            try
            {
                PruebasLaboratorio? pruebaslaboratorioToUpdate = await _patientManagerContext.PruebasLaboratorios.FindAsync(pruebasLaboratorio.PruebaID);

                pruebaslaboratorioToUpdate.PruebaID = pruebasLaboratorio.PruebaID;
                pruebaslaboratorioToUpdate.NombrePrueba = pruebasLaboratorio.NombrePrueba;

                result = await base.Update(pruebaslaboratorioToUpdate);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error actualizando la prueba.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> Remove(PruebasLaboratorio pruebasLaboratorio)
        {
            OperationResult result = new OperationResult();

            _pruebasLaboratorioValidations.ValidateRemove(pruebasLaboratorio);

            try
            {
                result = await base.Remove(pruebasLaboratorio);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error eliminando la prueba.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from laboratorio in _patientManagerContext.PruebasLaboratorios
                                     join atencionmedica in _patientManagerContext.Consultorios on laboratorio.ConsultorioID equals atencionmedica.ConsultorioID
                                     
                                     orderby laboratorio.PruebaID descending

                                     select new PruebasLaboratorioModel()
                                     {
                                         PruebaID = laboratorio.PruebaID,
                                         NombrePrueba = laboratorio.NombrePrueba,
                                         ConsultorioID = atencionmedica.ConsultorioID,

                                     }).AsNoTracking()
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo las pruebas.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> GetById(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from laboratorio in _patientManagerContext.PruebasLaboratorios
                                     join atencionmedica in _patientManagerContext.Consultorios on laboratorio.ConsultorioID equals atencionmedica.ConsultorioID

                                     where laboratorio.PruebaID == id

                                     select new PruebasLaboratorioModel()
                                     {
                                         PruebaID = laboratorio.PruebaID,
                                         NombrePrueba = laboratorio.NombrePrueba,
                                         ConsultorioID = atencionmedica.ConsultorioID,

                                     }).AsNoTracking()
                                     .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo la prueba.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }
    }
}
