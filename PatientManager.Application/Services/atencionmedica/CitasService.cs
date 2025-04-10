
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PatientManager.Application.Contracts.atencionmedica;
using PatientManager.Application.Core;
using PatientManager.Application.Dtos.administracion;
using PatientManager.Application.Dtos.atencionmedica;
using PatientManager.Application.Helpers.web;
using PatientManager.Domain.Entities.atencionmedica;
using PatientManager.Persistance.Interfaces.atencionmedica;
using PatientManager.Persistance.Models.ViewModel.atencionmedica;

namespace PatientManager.Application.Services.atencionmedica
{
    public class CitasService : ICitasService
    {
        private readonly ICitasRepository _citasRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ICitasService> _logger;
        private readonly CitasDto _citasDto;

        public CitasService(ICitasRepository citasRepository, ILogger<ICitasService> logger,
                            IHttpContextAccessor httpContextAccessor)
        {
            _citasRepository = citasRepository;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _citasDto = _httpContextAccessor.HttpContext.Session.Get<CitasDto>("usuario");
        }
        public async Task<ServiceResponse> GetAll()
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var usuarioSesion = _httpContextAccessor.HttpContext.Session.Get<UsuariosDto>("usuario");

                var result = await _citasRepository.GetAll();

                var citas = result.Data as List<CitaViewModel>;

                response.Model = citas
                    .Where(u => u.ConsultorioID == usuarioSesion.ConsultorioID)
                    .ToList();

                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error obteniendo las citas.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> GetByID(int id)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var result = await _citasRepository.GetById(id);

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
                response.Messages = "Ha ocurrido un error obteniendo la cita.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> RemoveAsync(CitasDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                Citas citas = new Citas();

                citas.CitaID = dto.CitaID;

                var result = await _citasRepository.Remove(citas);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error eliminando la cita.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> SaveAsync(CitasDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                Citas citas = new Citas();

                citas.PacienteID = dto.PacienteID;
                citas.MedicoID = dto.MedicoID;
                citas.Fecha = dto.Fecha;
                citas.Hora = dto.Hora;
                citas.Causa = dto.Causa;
                citas.EstadoID = dto.EstadoID;
                citas.ConsultorioID = _citasDto.ConsultorioID;

                var result = await _citasRepository.Save(citas);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error guardando la cita.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> UpdateAsync(CitasDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var resultGetBy = await _citasRepository.GetById(dto.CitaID);

                if (!resultGetBy.Success)
                {
                    response.IsSuccess = resultGetBy.Success;
                    response.Messages = resultGetBy.Message;

                    return response;
                }
                Citas citas = new Citas();

                citas.CitaID = dto.CitaID;
                citas.PacienteID = dto.PacienteID;
                citas.MedicoID = dto.MedicoID;
                citas.Fecha = dto.Fecha;
                citas.Hora = dto.Hora;
                citas.Causa = dto.Causa;
                citas.EstadoID = dto.EstadoID;
                citas.ConsultorioID = _citasDto.ConsultorioID;

                var result = await _citasRepository.Update(citas);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error eliminando la cita.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }
    }
}
