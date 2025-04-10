using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PatientManager.Application.Contracts.laboratorio;
using PatientManager.Application.Core;
using PatientManager.Application.Dtos.administracion;
using PatientManager.Application.Dtos.laboratorio;
using PatientManager.Application.Helpers.web;
using PatientManager.Domain.Entities.laboratorio;
using PatientManager.Persistance.Interfaces.laboratorio;
using PatientManager.Persistance.Models.laboratorio;
using PatientManager.Persistance.Models.ViewModel.laboratorio;

namespace PatientManager.Application.Services.laboratorio
{
    public class ResultadosLaboratorioService : IResultadosLaboratorioService
    {
        private readonly IResultadosLaboratorioRepository _resultadosLaboratorioRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<IResultadosLaboratorioService> _logger;
        public ResultadosLaboratorioService(IResultadosLaboratorioRepository resultadosLaboratorioRepository, ILogger<IResultadosLaboratorioService> logger,
                                            IHttpContextAccessor httpContextAccessor)
        {
            _resultadosLaboratorioRepository = resultadosLaboratorioRepository; 
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<ServiceResponse> GetAll()
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var usuarioSesion = _httpContextAccessor.HttpContext.Session.Get<UsuariosDto>("usuario");

                var result = await _resultadosLaboratorioRepository.GetAll();

                var resultadosLaboratorio = result.Data as List<ResultadosLaboratorioViewModel>;

                // Filtra solo los del consultorio del usuario y con EstadoID == 1
                response.Model = resultadosLaboratorio
                    .Where(r => r.ConsultorioID == usuarioSesion.ConsultorioID && r.EstadoID == 1)
                    .ToList();

                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error obteniendo los resultados.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }



        public async Task<ServiceResponse> GetByID(int id)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var result = await _resultadosLaboratorioRepository.GetById(id);

                if (!result.Success)
                {
                    response.IsSuccess = result.Success;
                    response.Messages = result.Message;

                    return response;
                }
                response.Model = result.Data;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error obteniendo el resultado.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> GetResultadosByPatientAsync(int id)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var result = await _resultadosLaboratorioRepository.GetResultadosByPatient(id);

                if (!result.Success)
                {
                    response.IsSuccess = result.Success;
                    response.Messages = result.Message;

                    return response;
                }
                response.Model = result.Data;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error obteniendo los resultados del paciente.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> RemoveAsync(ResultadosLaboratorioDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                ResultadosLaboratorio resultadosLaboratorio = new ResultadosLaboratorio();

                resultadosLaboratorio.ResultadoID = dto.ResultadoID;

                var result = await _resultadosLaboratorioRepository.Remove(resultadosLaboratorio);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error eliminando el resultado.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> SaveAsync(ResultadosLaboratorioDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                ResultadosLaboratorio resultadosLaboratorio = new ResultadosLaboratorio();

                resultadosLaboratorio.PruebaID = dto.PruebaID;
                resultadosLaboratorio.Resultado = dto.Resultado;
                resultadosLaboratorio.PacienteID = dto.PacienteID;
                resultadosLaboratorio.EstadoID = dto.EstadoID;
                resultadosLaboratorio.ConsultorioID = dto.ConsultorioID;

                var result = await _resultadosLaboratorioRepository.Save(resultadosLaboratorio);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error guardando el resultado.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> UpdateAsync(ResultadosLaboratorioDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var resultGetBy = await _resultadosLaboratorioRepository.GetById(dto.ResultadoID);

                if (!resultGetBy.Success)
                {
                    response.IsSuccess = resultGetBy.Success;
                    response.Messages = resultGetBy.Message;

                    return response;
                }

                ResultadosLaboratorio resultadosLaboratorio = new ResultadosLaboratorio();

                resultadosLaboratorio.ResultadoID = dto.ResultadoID;
                resultadosLaboratorio.PruebaID = dto.PruebaID;
                resultadosLaboratorio.Resultado = dto.Resultado;
                resultadosLaboratorio.EstadoID = dto.EstadoID;
                resultadosLaboratorio.PacienteID = dto.PacienteID;

                var result = await _resultadosLaboratorioRepository.Update(resultadosLaboratorio);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error actualizando el resultado.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }
    }
}
