
using PatientManager.Domain.Entities.atencionmedica;
using PatientManager.Domain.Result;
using PatientManager.Persistance.Base;
using PatientManager.Persistance.Context;
using PatientManager.Persistance.Interfaces.atencionmedica;
using PatientManager.Persistance.Models.atencionmedica;
using PatientManager.Persistance.Validations.atencionmedica;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PatientManager.Persistance.Models.ViewModel.atencionmedica;

namespace PatientManager.Persistance.Repositories.atencionmedica
{
    public sealed class CitasRepository(PatientManagerContext patientManagerContext,
                 ILogger<CitasRepository> logger, CitasValidations citasValidations) : BaseRepository<Citas>(patientManagerContext), ICitasRepository
    {
        private readonly PatientManagerContext _patientManagerContext = patientManagerContext;
        private readonly ILogger<CitasRepository> _logger = logger;
        private readonly CitasValidations _citasvalidations = citasValidations;

        public async override Task<OperationResult> Save(Citas citas)
        {
            OperationResult result = new OperationResult();

            _citasvalidations.ValidateSave(citas);

            try
            {
                result = await base.Save(citas);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error guardando la cita.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> Update(Citas citas)
        {
            OperationResult result = new OperationResult();

            _citasvalidations.ValidateUpdate(citas);

            try
            {
                Citas? citasToUpdate = await _patientManagerContext.Citas.FindAsync(citas.CitaID);

                citasToUpdate.CitaID = citas.CitaID;
                citasToUpdate.PacienteID = citas.PacienteID;
                citasToUpdate.MedicoID = citas.MedicoID;
                citasToUpdate.Fecha = citas.Fecha;
                citasToUpdate.Hora = citas.Hora;
                citasToUpdate.Causa = citas.Causa;
                citasToUpdate.EstadoID = citas.EstadoID;
                citasToUpdate.ConsultorioID = citas.ConsultorioID;

                result = await base.Update(citasToUpdate);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error actualizando la cita.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> Remove(Citas citas)
        {
            OperationResult result = new OperationResult();

            _citasvalidations.ValidateRemove(citas);

            try
            {
                result = await base.Remove(citas);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error eliminando la cita.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from atencionmedica in _patientManagerContext.Citas
                                     join atencionmedica1 in _patientManagerContext.Pacientes on atencionmedica.PacienteID equals atencionmedica1.PacienteID
                                     join atencionmedica2 in _patientManagerContext.Medicos on atencionmedica.MedicoID equals atencionmedica2.MedicoID
                                     join administracion in _patientManagerContext.Estados on atencionmedica.EstadoID equals administracion.EstadoID
                                     join administracion1 in _patientManagerContext.Consultorios on atencionmedica.ConsultorioID equals administracion1.ConsultorioID

                                     orderby atencionmedica.CitaID descending

                                     select new CitaViewModel()
                                     {
                                         CitaID = atencionmedica.CitaID,
                                         NombrePaciente = atencionmedica1.Nombre,
                                         NombreMedico = atencionmedica2.Nombre,
                                         FechaCita = atencionmedica.Fecha,
                                         Hora = atencionmedica.Hora,
                                         Causa = atencionmedica.Causa,
                                         Estado = administracion.Nombre,
                                         ConsultorioID = administracion1.ConsultorioID

                                     }).AsNoTracking()
                                     .ToListAsync();


            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo las citas.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> GetById(int id)
        {
            OperationResult result = new OperationResult();

            try
            { 
                result.Data = await (from atencionmedica in _patientManagerContext.Citas
                                     join atencionmedica1 in _patientManagerContext.Pacientes on atencionmedica.PacienteID equals atencionmedica1.PacienteID
                                     join atencionmedica2 in _patientManagerContext.Medicos on atencionmedica.MedicoID equals atencionmedica2.MedicoID
                                     join administracion in _patientManagerContext.Estados on atencionmedica.EstadoID equals administracion.EstadoID
                                     join administracion1 in _patientManagerContext.Consultorios on atencionmedica.ConsultorioID equals administracion1.ConsultorioID

                                     where atencionmedica.CitaID == id

                                     select new CitasModel()
                                     {
                                         CitaID = atencionmedica.CitaID,
                                         PacienteID = atencionmedica1.PacienteID,
                                         MedicoID = atencionmedica2.MedicoID,
                                         Fecha = atencionmedica.Fecha,
                                         Hora = atencionmedica.Hora,
                                         Causa = atencionmedica.Causa,
                                         EstadoID = administracion.EstadoID,
                                         ConsultorioID = administracion1.ConsultorioID

                                     }).AsNoTracking()
                                     .FirstOrDefaultAsync();


            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo la cita.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }
    }
}
