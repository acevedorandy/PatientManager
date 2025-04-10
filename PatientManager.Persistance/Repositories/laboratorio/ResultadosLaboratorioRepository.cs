
using PatientManager.Domain.Entities.laboratorio;
using PatientManager.Domain.Result;
using PatientManager.Persistance.Base;
using PatientManager.Persistance.Context;
using PatientManager.Persistance.Interfaces.laboratorio;
using PatientManager.Persistance.Models.laboratorio;
using PatientManager.Persistance.Validations.laboratorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PatientManager.Persistance.Models.ViewModel.laboratorio;

namespace PatientManager.Persistance.Repositories.laboratorio
{
    public sealed class ResultadosLaboratorioRepository(PatientManagerContext patientManagerContext,
                 ILogger<ResultadosLaboratorioRepository> logger, ResultadosLaboratorioValidations resultadosLaboratorioValidations) :BaseRepository<ResultadosLaboratorio>(patientManagerContext), IResultadosLaboratorioRepository
    {
        private readonly PatientManagerContext _patientManagerContext = patientManagerContext;
        private readonly ILogger<ResultadosLaboratorioRepository> _logger = logger;
        private readonly ResultadosLaboratorioValidations _resultadosLaboratorioValidations = resultadosLaboratorioValidations;

        public async override Task<OperationResult> Save(ResultadosLaboratorio resultadosLaboratorio)
        {
            OperationResult result = new OperationResult();

            _resultadosLaboratorioValidations.ValidateSave(resultadosLaboratorio);

            try
            {
                result = await base.Save(resultadosLaboratorio);
            }
            catch (Exception ex)
            {
                result.Success= false;
                result.Message = "Ha ocurrido un error guardando el resultado.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> Update(ResultadosLaboratorio resultadosLaboratorio)
        {
            OperationResult result = new OperationResult();

            _resultadosLaboratorioValidations.ValidateUpdate(resultadosLaboratorio);

            try
            {
                ResultadosLaboratorio? resultadoslaboratorioToUpdate = await _patientManagerContext.ResultadosLaboratorios.FindAsync(resultadosLaboratorio.ResultadoID);

                resultadoslaboratorioToUpdate.ResultadoID = resultadosLaboratorio.ResultadoID;
                resultadoslaboratorioToUpdate.PruebaID = resultadosLaboratorio.PruebaID;
                resultadoslaboratorioToUpdate.Resultado = resultadosLaboratorio.Resultado;
                resultadoslaboratorioToUpdate.EstadoID = resultadosLaboratorio.EstadoID;
                resultadoslaboratorioToUpdate.PacienteID = resultadosLaboratorio.PacienteID;

                result = await base.Update(resultadoslaboratorioToUpdate);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error actualizando el resultado.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> Remove(ResultadosLaboratorio resultadosLaboratorio)
        {
            OperationResult result = new OperationResult();

            _resultadosLaboratorioValidations.ValidateRemove(resultadosLaboratorio);

            try
            {
                result = await base.Remove(resultadosLaboratorio);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error eliminando el resultado.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from administracion in _patientManagerContext.ResultadosLaboratorios
                                     join paciente in _patientManagerContext.Pacientes on administracion.PacienteID equals paciente.PacienteID
                                     join prueba in _patientManagerContext.PruebasLaboratorios on administracion.PruebaID equals prueba.PruebaID
                                     join estado in _patientManagerContext.Estados on administracion.EstadoID equals estado.EstadoID
                                     join consultorio in _patientManagerContext.Consultorios on administracion.ConsultorioID equals consultorio.ConsultorioID

                                     orderby administracion.ResultadoID descending

                                     select new ResultadosLaboratorioViewModel()
                                     {
                                         ResultadoID = administracion.ResultadoID,
                                         Nombre = paciente.Nombre,
                                         Apellido = paciente.Apellido,
                                         Cedula = paciente.Cedula,
                                         NombrePrueba = prueba.NombrePrueba,
                                         EstadoID = estado.EstadoID,
                                         ConsultorioID = consultorio.ConsultorioID

                                     }).AsNoTracking()
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo los resultados.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> GetById(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from laboratorio in _patientManagerContext.ResultadosLaboratorios
                                     join laboratorio1 in _patientManagerContext.PruebasLaboratorios on laboratorio.PruebaID equals laboratorio1.PruebaID
                                     join administracion in _patientManagerContext.Estados on laboratorio.EstadoID equals administracion.EstadoID
                                     join atencionmedica in _patientManagerContext.Pacientes on laboratorio.PacienteID equals atencionmedica.PacienteID
                                     join administracion1 in _patientManagerContext.Consultorios on laboratorio.ConsultorioID equals administracion1.ConsultorioID

                                     where laboratorio.ResultadoID == id

                                     select new ResultadosLaboratorioModel()
                                     {
                                         ResultadoID = laboratorio.ResultadoID,
                                         PruebaID = laboratorio1.PruebaID,
                                         Resultado = laboratorio.Resultado,
                                         EstadoID = administracion.EstadoID,
                                         PacienteID = atencionmedica.PacienteID,
                                         ConsultorioID = administracion1.ConsultorioID,


                                     }).AsNoTracking()
                                     .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo el resultado.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async Task<OperationResult> GetResultadosByPatient(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from laboratorio in _patientManagerContext.ResultadosLaboratorios
                                     join laboratorio1 in _patientManagerContext.PruebasLaboratorios on laboratorio.PruebaID equals laboratorio1.PruebaID
                                     join administracion in _patientManagerContext.Estados on laboratorio.EstadoID equals administracion.EstadoID

                                     where laboratorio.PacienteID == id

                                     select new ResultadosViewModelByPatient()
                                     {
                                         ResultadoID = laboratorio.ResultadoID,
                                         PacienteID = laboratorio.PacienteID,
                                         PruebaID = laboratorio1.PruebaID,
                                         NombrePrueba = laboratorio1.NombrePrueba,
                                         Estado = administracion.Nombre,
                                         Resultados = laboratorio != null ? laboratorio.Resultado : null

                                     }).AsNoTracking()
                                     .ToListAsync();
            }
            catch (Exception)
            {
            }
            return result;
        }
    }
}
