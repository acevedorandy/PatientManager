
using PatientManager.Domain.Entities.atencionmedica;
using PatientManager.Domain.Result;
using PatientManager.Persistance.Base;
using PatientManager.Persistance.Context;
using PatientManager.Persistance.Interfaces.atencionmedica;
using PatientManager.Persistance.Models.atencionmedica;
using PatientManager.Persistance.Validations.atencionmedica;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PatientManager.Persistance.Repositories.atencionmedica
{
    public sealed class MedicosRepository(PatientManagerContext patientManagerContext,
                 ILogger<MedicosRepository> logger, MedicosValidations medicosValidations) : BaseRepository<Medicos>(patientManagerContext), IMedicosRepository
    {
        private readonly PatientManagerContext _patientManagerContext = patientManagerContext;
        private readonly ILogger<MedicosRepository> _logger = logger;
        private readonly MedicosValidations _medicosValidations = medicosValidations;

        public async override Task<OperationResult> Save(Medicos medicos)
        {
            OperationResult result = new OperationResult();

            _medicosValidations.ValidateSave(medicos);

            try
            {
                result = await base.Save(medicos);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error guardando el medico.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> Update(Medicos medicos)
        {
            OperationResult result = new OperationResult();

            _medicosValidations.ValidateUpdate(medicos);

            try
            {
                Medicos? medicosToUpdate = await _patientManagerContext.Medicos.FindAsync(medicos.MedicoID);

                medicosToUpdate.MedicoID = medicos.MedicoID;
                medicosToUpdate.Nombre = medicos.Nombre;
                medicosToUpdate.Apellido = medicos.Apellido;
                medicosToUpdate.Correo = medicos.Correo;
                medicosToUpdate.Telefono = medicos.Telefono;
                medicosToUpdate.Cedula = medicos.Cedula;
                medicosToUpdate.Foto = medicos.Foto;
                medicosToUpdate.ConsultorioID = medicos.ConsultorioID;

                result = await base.Update(medicosToUpdate);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error actualizando el medico.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> Remove(Medicos medicos)
        {
            OperationResult result = new OperationResult();

            _medicosValidations.ValidateRemove(medicos);

            try
            {
                result = await base.Remove(medicos);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error eliminando el medico.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from atencionmedica in _patientManagerContext.Medicos
                                     join administracion in _patientManagerContext.Consultorios on atencionmedica.ConsultorioID equals administracion.ConsultorioID

                                     orderby atencionmedica.MedicoID descending

                                     select new MedicosModel()
                                     {
                                         MedicoID = atencionmedica.MedicoID,
                                         Nombre = atencionmedica.Nombre,
                                         Apellido = atencionmedica.Apellido,
                                         Correo = atencionmedica.Correo,
                                         Telefono = atencionmedica.Telefono,
                                         Cedula = atencionmedica.Cedula,
                                         Foto = atencionmedica.Foto ?? (string?)null,
                                         ConsultorioID = administracion.ConsultorioID

                                     }).AsNoTracking()
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo los medicos.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> GetById(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from atencionmedica in _patientManagerContext.Medicos
                                     join administracion in _patientManagerContext.Consultorios on atencionmedica.ConsultorioID equals administracion.ConsultorioID

                                     where atencionmedica.MedicoID == id

                                     select new MedicosModel()
                                     {
                                         MedicoID = atencionmedica.MedicoID,
                                         Nombre = atencionmedica.Nombre,
                                         Apellido = atencionmedica.Apellido,
                                         Correo = atencionmedica.Correo,
                                         Telefono = atencionmedica.Telefono,
                                         Cedula = atencionmedica.Cedula,
                                         Foto = atencionmedica.Foto ?? (string?)null,
                                         ConsultorioID = administracion.ConsultorioID

                                     }).AsNoTracking()
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo los medicos.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }
    }
}
