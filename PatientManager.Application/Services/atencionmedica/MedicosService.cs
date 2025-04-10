
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
    public class MedicosService : IMedicosService
    {
        private readonly IMedicosRepository _medicosRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<IMedicosService> _logger;
        private readonly MedicosDto _medicosDto;

        public MedicosService(IMedicosRepository medicosRepository, ILogger<IMedicosService> logger,
                                IHttpContextAccessor httpContextAccessor)
        {
            _medicosRepository = medicosRepository;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _medicosDto = _httpContextAccessor.HttpContext.Session.Get<MedicosDto>("usuario");
        }
        public async Task<ServiceResponse> GetAll()
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var usuarioSesion = _httpContextAccessor.HttpContext.Session.Get<UsuariosDto>("usuario");

                var result = await _medicosRepository.GetAll();

                var medicos = result.Data as List<MedicosModel>;

                response.Model = medicos
                    .Where(m => m.ConsultorioID == usuarioSesion.ConsultorioID)
                    .ToList();

                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error obteniendo los medicos.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> GetByID(int id)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var result = await _medicosRepository.GetById(id);

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
                response.Messages = "Ha ocurrido un error obteniendo el medico.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> GetDoctorConvertion(int id)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var result = await _medicosRepository.GetById(id);

                if (!result.Success || result.Data == null)
                {
                    response.IsSuccess = false;
                    response.Messages = result.Message ?? "Médico no encontrado.";
                    return response;
                }

                var medicoModel = (MedicosModel)result.Data;
                var medicoDto = new MedicosDto()
                {
                    MedicoID = medicoModel.MedicoID,
                    Nombre = medicoModel.Nombre,
                    Apellido = medicoModel.Apellido,
                    Correo = medicoModel.Correo,
                    Telefono = medicoModel.Telefono,
                    Cedula = medicoModel.Cedula,
                    Foto = medicoModel.Foto,
                    ConsultorioID = medicoModel.ConsultorioID
                };

                response.IsSuccess = true;
                response.Model = medicoDto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error obteniendo el médico.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> RemoveAsync(MedicosDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                Medicos medicos = new Medicos();

                medicos.MedicoID = dto.MedicoID;

                var result = await _medicosRepository.Remove(medicos);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error eliminando el medico.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> SaveAsync(MedicosDto dto)
        {
            ServiceResponse response = new();

            try
            {
                Medicos medicos = new Medicos();

                medicos.Nombre = dto.Nombre;
                medicos.Apellido = dto.Apellido;
                medicos.Correo = dto.Correo;
                medicos.Telefono = dto.Telefono;
                medicos.Cedula = dto.Cedula;
                medicos.Foto = dto.Foto;
                medicos.ConsultorioID = _medicosDto.ConsultorioID;

                var result = await _medicosRepository.Save(medicos);

                dto.MedicoID = medicos.MedicoID;
                dto.Nombre = medicos.Nombre;
                dto.Apellido = medicos.Apellido;
                dto.Correo = medicos.Correo;
                dto.Telefono = medicos.Telefono;
                dto.Cedula = medicos.Cedula;
                dto.Foto = medicos.Foto;
                dto.ConsultorioID = _medicosDto.ConsultorioID;

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

        public async Task<ServiceResponse> UpdateAsync(MedicosDto dto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var resultGetBy = await _medicosRepository.GetById(dto.MedicoID);

                if (!resultGetBy.Success)
                {
                    response.IsSuccess = resultGetBy.Success;
                    response.Messages = resultGetBy.Message;

                    return response;
                }
                Medicos medicos = new Medicos();

                medicos.MedicoID = dto.MedicoID;
                medicos.Nombre = dto.Nombre;
                medicos.Apellido = dto.Apellido;
                medicos.Correo = dto.Correo;
                medicos.Telefono = dto.Telefono;
                medicos.Cedula = dto.Cedula;
                medicos.Foto = dto.Foto;
                medicos.ConsultorioID = dto.ConsultorioID;

                var result = await _medicosRepository.Update(medicos);

                dto.MedicoID = medicos.MedicoID;
                dto.Nombre = medicos.Nombre;
                dto.Apellido = medicos.Apellido;
                dto.Correo = medicos.Correo;
                dto.Telefono = medicos.Telefono;
                dto.Cedula = medicos.Cedula;
                dto.Foto = medicos.Foto;
                dto.ConsultorioID = medicos.ConsultorioID;

                if (dto.File == null)
                {
                    Console.WriteLine("❌ El archivo se perdió después de SaveAsync.");
                }
                else
                {
                    Console.WriteLine($"✔️ Archivo sigue presente después de SaveAsync: {dto.File.FileName}");
                }

                response.Model = dto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error actualizando el medico.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }
    }
}

