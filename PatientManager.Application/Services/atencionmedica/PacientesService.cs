using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PatientManager.Application.Contracts.atencionmedica;
using PatientManager.Application.Core;
using PatientManager.Application.Dtos.administracion;
using PatientManager.Application.Dtos.atencionmedica;
using PatientManager.Application.Helpers.web;
using PatientManager.Domain.Entities.atencionmedica;
using PatientManager.Persistance.Interfaces.atencionmedica;
using PatientManager.Persistance.Models.atencionmedica;

namespace PatientManager.Application.Services.atencionmedica
{
    public class PacientesService : IPacientesService
    {
        private readonly IPacientesRepository _pacientesRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<IPacientesService> _logger;
        private readonly PacientesDto _pacientesDto;

        public PacientesService(IPacientesRepository pacientesRepository, ILogger<IPacientesService> logger,
                                IHttpContextAccessor httpContextAccessor)
        {
            _pacientesRepository = pacientesRepository;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _pacientesDto = _httpContextAccessor.HttpContext.Session.Get<PacientesDto>("usuario");
        }

        public async Task<ServiceResponse> GetAll()
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var usuarioSesion = _httpContextAccessor.HttpContext.Session.Get<UsuariosDto>("usuario");

                var result = await _pacientesRepository.GetAll();

                var pacientes = result.Data as List<PacientesModel>;

                response.Model = pacientes
                    .Where(p => p.ConsultorioID == usuarioSesion.ConsultorioID)
                    .ToList();

                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error obteniendo los pacientes.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> GetByID(int id)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var result = await _pacientesRepository.GetById(id);

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
                response.Messages = "Ha ocurrido un error obteniendo el paciente.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> GetPacienteConvertion(int id)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var result = await _pacientesRepository.GetById(id);

                if (!result.Success || result.Data == null) 
                {
                    response.IsSuccess = false;
                    response.Messages = result.Message ?? "Paciente no encontrado.";
                    return response;
                }

                var pacienteModel = (PacientesModel)result.Data;
                var pacienteDto = new PacientesDto()
                {
                    PacienteID = pacienteModel.PacienteID,
                    Nombre = pacienteModel.Nombre,
                    Apellido = pacienteModel.Apellido,
                    Telefono = pacienteModel.Telefono,
                    Direccion = pacienteModel.Direccion,
                    Cedula = pacienteModel.Cedula,
                    FechaNacimiento = pacienteModel.FechaNacimiento,
                    Fumador = pacienteModel.Fumador,
                    Alergias = pacienteModel.Alergias,
                    Foto = pacienteModel.Foto,
                    ConsultorioID = pacienteModel.ConsultorioID
                };

                response.IsSuccess = true;
                response.Model = pacienteDto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error obteniendo el paciente.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> RemoveAsync(PacientesDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                Pacientes pacientes = new Pacientes();

                pacientes.PacienteID = dto.PacienteID;

                var result = await _pacientesRepository.Remove(pacientes);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error eliminando el paciente.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> SaveAsync(PacientesDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                Pacientes pacientes = new Pacientes();

                pacientes.Nombre = dto.Nombre;
                pacientes.Apellido = dto.Apellido;
                pacientes.Telefono = dto.Telefono;
                pacientes.Direccion = dto.Direccion;
                pacientes.Cedula = dto.Cedula;
                pacientes.FechaNacimiento = dto.FechaNacimiento;
                pacientes.Fumador = dto.Fumador;
                pacientes.Alergias = dto.Alergias;
                pacientes.Foto = dto.Foto;
                pacientes.ConsultorioID = _pacientesDto.ConsultorioID;

                var result = await _pacientesRepository.Save(pacientes);

                dto.PacienteID = pacientes.PacienteID;
                dto.Nombre = pacientes.Nombre;
                dto.Telefono = pacientes.Telefono;
                dto.Direccion = dto.Direccion;
                dto.Cedula = dto.Cedula;
                dto.FechaNacimiento = dto.FechaNacimiento;
                dto.Fumador = dto.Fumador;
                dto.Alergias = dto.Alergias;
                dto.Foto = dto.Foto;
                dto.ConsultorioID = _pacientesDto.ConsultorioID;

                if (dto.File == null)
                {
                    Console.WriteLine("El archivo se perdió después de SaveAsync.");
                }
                else
                {
                    Console.WriteLine($"Archivo sigue presente después de SaveAsync: {dto.File.FileName}");
                }

                response.Model = dto;

                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error guardando el paciente.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> UpdateAsync(PacientesDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var resultGetBy = await _pacientesRepository.GetById(dto.PacienteID);

                if (!resultGetBy.Success)
                {
                    response.IsSuccess = resultGetBy.Success;
                    response.Messages = resultGetBy.Message;

                    return response;
                }

                Pacientes pacientes = new Pacientes();

                pacientes.PacienteID = dto.PacienteID;
                pacientes.Nombre = dto.Nombre;
                pacientes.Apellido = dto.Apellido;
                pacientes.Telefono = dto.Telefono;
                pacientes.Direccion = dto.Direccion;
                pacientes.Cedula = dto.Cedula;
                pacientes.FechaNacimiento = dto.FechaNacimiento;
                pacientes.Fumador = dto.Fumador;
                pacientes.Alergias = dto.Alergias;
                pacientes.Foto = dto.Foto;
                dto.ConsultorioID = _pacientesDto.ConsultorioID;

                var result = await _pacientesRepository.Update(pacientes);

                dto.PacienteID = pacientes.PacienteID;
                dto.Nombre = pacientes.Nombre;
                dto.Telefono = pacientes.Telefono;
                dto.Direccion = dto.Direccion;
                dto.Cedula = dto.Cedula;
                dto.FechaNacimiento = dto.FechaNacimiento;
                dto.Fumador = dto.Fumador;
                dto.Alergias = dto.Alergias;
                dto.Foto = dto.Foto;
                dto.ConsultorioID = _pacientesDto.ConsultorioID;

                response.Model = dto;

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error actualizando el paciente.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }
    }
}
