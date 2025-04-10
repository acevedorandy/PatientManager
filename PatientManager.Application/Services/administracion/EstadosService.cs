

using Microsoft.Extensions.Logging;
using PatientManager.Application.Contracts.administracion;
using PatientManager.Application.Core;
using PatientManager.Application.Dtos.administracion;
using PatientManager.Domain.Entities.administracion;
using PatientManager.Persistance.Interfaces.administracion;

namespace PatientManager.Application.Services.administracion
{
    public class EstadosService : IEstadosService
    {
        private readonly IEstadosRepository _estadosRepository;
        private readonly ILogger<IEstadosService> _logger;

        public EstadosService(IEstadosRepository estadosRepository, ILogger<IEstadosService> logger)
        {
            _estadosRepository = estadosRepository;
            _logger = logger;
        }
        public async Task<ServiceResponse> GetAll()
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var result = await _estadosRepository.GetAll();
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
                response.Messages = "Ha ocurrido un error obteniendo los estados.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> GetByID(int id)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var result = await _estadosRepository.GetById(id);

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
                response.Messages = "Ha ocurrido un error obteniendo el estado.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> RemoveAsync(EstadosDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                Estados estados = new Estados();

                estados.EstadoID = dto.EstadoID;
                estados.Nombre = dto.Nombre;

                var result = await _estadosRepository.Remove(estados);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error eliminando el estado.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> SaveAsync(EstadosDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                Estados estados = new Estados();

                estados.Nombre = dto.Nombre;

                var result = await _estadosRepository.Save(estados);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error guardando el estado.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> UpdateAsync(EstadosDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var resultGetBy = await _estadosRepository.GetById(dto.EstadoID);

                if (!resultGetBy.Success)
                {
                    response.IsSuccess = resultGetBy.Success;
                    response.Messages = resultGetBy.Message;

                    return response;
                }

                Estados estados = new Estados();

                estados.EstadoID = dto.EstadoID;
                estados.Nombre = dto.Nombre;

                var result = await _estadosRepository.Update(estados);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error actualizando el estado.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }
    }
}
