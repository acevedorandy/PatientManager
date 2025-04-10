using PatientManager.Application.Core;
using PatientManager.Persistance.Interfaces.administracion;
using Microsoft.Extensions.Logging;
using PatientManager.Application.Contracts.administracion;
using PatientManager.Application.Dtos.administracion;
using PatientManager.Domain.Entities.administracion;
using Microsoft.AspNetCore.Http;
using PatientManager.Application.Helpers.web;
using PatientManager.Persistance.Models.administracion;

namespace PatientManager.Application.Services.administracion
{
    public class ConsultoriosService : IConsultoriosServices
    {
        private readonly IConsultoriosRepository _consultoriosRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<IConsultoriosServices> _logger;

        public ConsultoriosService(IConsultoriosRepository consultoriosRepository, ILogger<IConsultoriosServices> logger, 
                                    IHttpContextAccessor httpContextAccessor)
        {
            _consultoriosRepository = consultoriosRepository;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async Task<ServiceResponse> GetAll()
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var usuarioSesion = _httpContextAccessor.HttpContext.Session.Get<UsuariosDto>("usuario");

                var result = await _consultoriosRepository.GetAll();

                var consultorio = result.Data as List<ConsultoriosModel>;

                response.Model = consultorio
                    .Where(c => c.ConsultorioID == usuarioSesion.ConsultorioID)
                    .ToList();

                response.IsSuccess = true;

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error obteniendo los consultorios.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> GetByID(int id)
        {
            ServiceResponse response = new ServiceResponse();
            
            try
            {
                var result = await _consultoriosRepository.GetById(id);

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
                response.Messages = "Ha ocurrido un error obteniendo el consultorio.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> RemoveAsync(ConsultoriosDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                Consultorios consultorios = new Consultorios();

                consultorios.ConsultorioID = dto.ConsultorioID;
                consultorios.NombreConsultorio = dto.NombreConsultorio;

                var result = await _consultoriosRepository.Remove(consultorios);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error eliminando el consultorio.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> SaveAsync(ConsultoriosDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                Consultorios consultorios = new Consultorios();

                consultorios.NombreConsultorio = dto.NombreConsultorio;

                var result = await _consultoriosRepository.Save(consultorios);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error guardando el consultorio.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> UpdateAsync(ConsultoriosDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var resultGetBy = await _consultoriosRepository.GetById(dto.ConsultorioID);
               
                if (!resultGetBy.Success)
                {
                    response.IsSuccess = resultGetBy.Success;
                    response.Messages = resultGetBy.Message;

                    return response;
                }

                Consultorios consultorios = new Consultorios();

                consultorios.ConsultorioID = dto.ConsultorioID;
                consultorios.NombreConsultorio = dto.NombreConsultorio;

                var result = await _consultoriosRepository.Update(consultorios);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error actualizando el consultorio.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }
    }
}
