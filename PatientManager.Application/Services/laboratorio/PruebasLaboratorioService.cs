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

namespace PatientManager.Application.Services.laboratorio
{
    public class PruebasLaboratorioService : IPruebasLaboratorioService
    {
        private readonly IPruebasLaboratorioRepository _pruebasLaboratorioRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ILogger<IPruebasLaboratorioService> _logger;

        public PruebasLaboratorioService(IPruebasLaboratorioRepository pruebasLaboratorioRepository, ILogger<IPruebasLaboratorioService> logger,
                                         IHttpContextAccessor httpContextAccessor)
        {
            _pruebasLaboratorioRepository = pruebasLaboratorioRepository; 
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<ServiceResponse> GetAll()
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var usuarioSesion = _httpContextAccessor.HttpContext.Session.Get<UsuariosDto>("usuario");

                var result = await _pruebasLaboratorioRepository.GetAll();

                var pruebasLaboratorio = result.Data as List<PruebasLaboratorioModel>;

                response.Model = pruebasLaboratorio
                    .Where(p => p.ConsultorioID == usuarioSesion.ConsultorioID)
                    .ToList();

                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error obteniendo las pruebas.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> GetByID(int id)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var result = await _pruebasLaboratorioRepository.GetById(id);

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
                response.Messages = "Ha ocurrido un error obteniendo la prueba.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> RemoveAsync(PruebasLaboratorioDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                PruebasLaboratorio pruebasLaboratorio = new PruebasLaboratorio();

                pruebasLaboratorio.PruebaID = dto.PruebaID;

                var result = await _pruebasLaboratorioRepository.Remove(pruebasLaboratorio);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error eliminando la prueba.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> SaveAsync(PruebasLaboratorioDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                PruebasLaboratorio pruebasLaboratorio = new PruebasLaboratorio();

                pruebasLaboratorio.NombrePrueba = dto.NombrePrueba;
                pruebasLaboratorio.ConsultorioID = dto.ConsultorioID;

                var result = await _pruebasLaboratorioRepository.Save(pruebasLaboratorio);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error guardando la prueba.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> UpdateAsync(PruebasLaboratorioDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var resultGetBy = await _pruebasLaboratorioRepository.GetById(dto.PruebaID);

                if (!resultGetBy.Success)
                {
                    response.IsSuccess = resultGetBy.Success;
                    response.Messages = resultGetBy.Message;

                    return response;
                }
                PruebasLaboratorio pruebasLaboratorio = new PruebasLaboratorio();

                pruebasLaboratorio.PruebaID = dto.PruebaID;
                pruebasLaboratorio.NombrePrueba = dto.NombrePrueba;

                var result = await _pruebasLaboratorioRepository.Update(pruebasLaboratorio);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error actualizando la prueba.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }
    }
}
