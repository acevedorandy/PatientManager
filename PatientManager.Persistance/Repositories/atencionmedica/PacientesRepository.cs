
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
    public sealed class PacientesRepository(PatientManagerContext patientManagerContext,
                 ILogger<PacientesRepository> logger, PacientesValidations pacientesValidations) : BaseRepository<Pacientes>(patientManagerContext), IPacientesRepository
    {
        private readonly PatientManagerContext _patientManagerContext = patientManagerContext;
        private readonly ILogger<PacientesRepository> _logger = logger;
        private readonly PacientesValidations _pacientesValidations = pacientesValidations;

        public async override Task<OperationResult> Save(Pacientes pacientes)
        {
            OperationResult result = new OperationResult();

            _pacientesValidations.ValidateSave(pacientes);

            try
            {
                result = await base.Save(pacientes);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error guardando el paciente.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> Update(Pacientes pacientes)
        {
            OperationResult result = new OperationResult();

            _pacientesValidations.ValidateUpdate(pacientes);

            try
            {
                Pacientes? pacientesToUpdate = await _patientManagerContext.Pacientes.FindAsync(pacientes.PacienteID);

                pacientesToUpdate.PacienteID = pacientes.PacienteID;
                pacientesToUpdate.Nombre = pacientes.Nombre;
                pacientesToUpdate.Apellido = pacientes.Apellido;
                pacientesToUpdate.Telefono = pacientes.Telefono;
                pacientesToUpdate.Direccion = pacientes.Direccion;
                pacientesToUpdate.Cedula = pacientes.Cedula;
                pacientesToUpdate.FechaNacimiento = pacientes.FechaNacimiento;
                pacientesToUpdate.Fumador = pacientes.Fumador;
                pacientesToUpdate.Alergias = pacientes.Alergias;
                pacientesToUpdate.Foto = pacientes.Foto;

                result = await base.Update(pacientesToUpdate);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error actualizando el paciente";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> Remove(Pacientes pacientes)
        {
            OperationResult result = new OperationResult();

            _pacientesValidations.ValidateRemove(pacientes);

            try
            {
                result = await base.Remove(pacientes);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error eliminando el paciente";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from atencionmedica in _patientManagerContext.Pacientes
                                     join administracion in _patientManagerContext.Consultorios on atencionmedica.ConsultorioID equals administracion.ConsultorioID
                                    
                                     orderby atencionmedica.PacienteID descending

                                     select new PacientesModel()
                                     {
                                         PacienteID = atencionmedica.PacienteID,
                                         Nombre = atencionmedica.Nombre,
                                         Apellido = atencionmedica.Apellido,
                                         Telefono = atencionmedica.Telefono,
                                         Direccion = atencionmedica.Direccion,
                                         Cedula = atencionmedica.Cedula,
                                         FechaNacimiento = atencionmedica.FechaNacimiento,
                                         Fumador = atencionmedica.Fumador,
                                         Alergias = atencionmedica.Alergias,
                                         Foto = atencionmedica.Foto,
                                         ConsultorioID = administracion.ConsultorioID

                                     }).AsNoTracking()
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo los pacientes.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> GetById(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from atencionmedica in _patientManagerContext.Pacientes
                                     join administracion in _patientManagerContext.Consultorios on atencionmedica.ConsultorioID equals administracion.ConsultorioID
                                     
                                     where atencionmedica.PacienteID == id  

                                     select new PacientesModel()
                                     {
                                         PacienteID = atencionmedica.PacienteID,
                                         Nombre = atencionmedica.Nombre,
                                         Apellido = atencionmedica.Apellido,
                                         Telefono = atencionmedica.Telefono,
                                         Direccion = atencionmedica.Direccion,
                                         Cedula = atencionmedica.Cedula,
                                         FechaNacimiento = atencionmedica.FechaNacimiento,
                                         Fumador = atencionmedica.Fumador,
                                         Alergias = atencionmedica.Alergias,
                                         Foto = atencionmedica.Foto,
                                         ConsultorioID = administracion.ConsultorioID

                                     }).AsNoTracking()
                                     .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo los pacientes.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }
    }
}
