using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PatientManager.Application.Contracts.administracion;
using PatientManager.Application.Core;
using PatientManager.Application.Dtos.administracion;
using PatientManager.Application.Helpers.web;
using PatientManager.Domain.Entities.administracion;
using PatientManager.Persistance.Context;
using PatientManager.Persistance.Interfaces.administracion;
using PatientManager.Persistance.Models.administracion;
using PatientManager.Persistance.Models.ViewModel.administracion.usuario;

namespace PatientManager.Application.Services.administracion
{
    public class UsuariosService : IUsuariosService
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IConsultoriosRepository _consultoriosRepository;
        private readonly PatientManagerContext _patientManagerContext;
        private ILogger<IUsuariosService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UsuariosDto _usuariosDto;

        public UsuariosService(IUsuariosRepository usuariosRepository, PatientManagerContext patientManagerContext, 
            IConsultoriosRepository consultoriosRepository, ILogger<IUsuariosService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _usuariosRepository = usuariosRepository;
            _consultoriosRepository = consultoriosRepository;
            _patientManagerContext = patientManagerContext;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _usuariosDto = _httpContextAccessor.HttpContext.Session.Get<UsuariosDto>("usuario");
        }
        public async Task<ServiceResponse> GetAll()
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var usuarioSesion = _httpContextAccessor.HttpContext.Session.Get<UsuariosDto>("usuario");

                var result = await _usuariosRepository.GetAll();

                var usuarios = result.Data as List<UsuariosModel>;

                response.Model = usuarios
                    .Where(u => u.ConsultorioID == usuarioSesion.ConsultorioID)
                    .ToList();

                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error obteniendo los usuarios.";
                _logger.LogError(response.Messages, ex.ToString());
            }

            return response;
        }


        public async Task<ServiceResponse> GetByID(int id)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var result = await _usuariosRepository.GetById(id);
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
                response.Messages = "Ha ocurrido un error obteniendo el usuario.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<UsuariosViewModel> Login(LogInUserViewModel logInUser)
        {
            try
            {
                Usuarios usuarios = await _usuariosRepository.Login(logInUser);

                if (usuarios == null)
                {
                    return null;
                }

                UsuariosViewModel userViewModel = new();

                userViewModel.UsuarioID = usuarios.UsuarioID;
                userViewModel.Nombre = usuarios.Nombre;
                userViewModel.Apellido = usuarios.Apellido;
                userViewModel.Correo = usuarios.Correo;
                userViewModel.NombreUsuario = usuarios.NombreUsuario;
                userViewModel.Contraseña = usuarios.Contraseña;
                userViewModel.TipoUsuario = usuarios.TipoUsuario;
                userViewModel.ConsultorioID = usuarios.ConsultorioID;

                return userViewModel;

            }
            catch (Exception ex)
            {
                _logger.LogError("Ha ocurrido un error iniciando sesión.: {Error}", ex.ToString());
                return null;
            }
        }

        public async Task<ServiceResponse> RemoveAsync(SaveUserViewModel saveUserViewModel)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                Usuarios usuarios = new Usuarios();

                usuarios.UsuarioID = saveUserViewModel.UsuarioID;

                var result = await _usuariosRepository.Remove(usuarios);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error eliminando el usuario.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> SaveAsistenteOnly(UsuariosDto usuariosDto)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                Usuarios usuarios = new Usuarios();
                
                usuarios.Nombre = usuariosDto.Nombre;
                usuarios.Apellido = usuariosDto.Apellido;
                usuarios.Correo = usuariosDto.Correo;
                usuarios.NombreUsuario = usuariosDto.NombreUsuario;
                usuarios.Contraseña = usuariosDto.Contraseña;
                usuarios.TipoUsuario = usuariosDto.TipoUsuario;
                usuarios.ConsultorioID = _usuariosDto.ConsultorioID;

                var result = await _usuariosRepository.Save(usuarios);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error eliminando el usuario.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }

        public async Task<ServiceResponse> SaveAsync(SaveUserViewModel saveUserViewModel)
        {
            ServiceResponse response = new ServiceResponse();

            using (var transaction = await _patientManagerContext.Database.BeginTransactionAsync())
            {
                try
                {
                    Consultorios consultorio = new Consultorios
                    {
                        NombreConsultorio = saveUserViewModel.NombreConsultorio
                    };

                    var resultConsultorio = await _consultoriosRepository.Save(consultorio);

                    if (!resultConsultorio.Success)
                    {
                        response.IsSuccess = false;
                        response.Messages = "Error guardando el consultorio.";
                        return response;
                    }

                    Usuarios usuarios = new Usuarios
                    {
                        Nombre = saveUserViewModel.Nombre,
                        Apellido = saveUserViewModel.Apellido,
                        Correo = saveUserViewModel.Correo,
                        NombreUsuario = saveUserViewModel.NombreUsuario,
                        Contraseña = saveUserViewModel.Contraseña,
                        TipoUsuario = saveUserViewModel.TipoUsuario,
                        ConsultorioID = consultorio.ConsultorioID 
                    };

                    var resultUsuario = await _usuariosRepository.Save(usuarios);

                    if (!resultUsuario.Success)
                    {
                        response.IsSuccess = false;
                        response.Messages = "Error guardando el usuario.";
                        return response;
                    }

                    await transaction.CommitAsync();

                    response.IsSuccess = true;
                    response.Messages = "Usuario y consultorio guardados correctamente.";
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    response.IsSuccess = false;
                    response.Messages = "Ha ocurrido un error guardando el usuario y consultorio.";
                    _logger.LogError(response.Messages, ex.ToString());
                }
            }

            return response;
        }


        public async Task<ServiceResponse> UpdateAsync(SaveUserViewModel saveUserViewModel)
        {
            ServiceResponse response = new ServiceResponse();
            Consultorios consultorios = new Consultorios();

            try
            {
                var resultGetBy = await _usuariosRepository.GetById(saveUserViewModel.UsuarioID);

                if (!resultGetBy.Success)
                {
                    response.IsSuccess = resultGetBy.Success;
                    response.Messages = resultGetBy.Message;

                    return response;
                }

                Usuarios usuarios = new Usuarios();

                usuarios.UsuarioID = saveUserViewModel.UsuarioID;
                usuarios.Nombre = saveUserViewModel.Nombre;
                usuarios.Apellido = saveUserViewModel.Apellido;
                usuarios.Correo = saveUserViewModel.Correo;
                usuarios.NombreUsuario = saveUserViewModel.NombreUsuario;
                usuarios.Contraseña = saveUserViewModel.Contraseña;
                usuarios.TipoUsuario = saveUserViewModel.TipoUsuario;
                usuarios.ConsultorioID = _usuariosDto.ConsultorioID;

                var result = await _usuariosRepository.Update(usuarios);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Messages = "Ha ocurrido un error actualizando el usuario.";
                _logger.LogError(response.Messages, ex.ToString());
            }
            return response;
        }
    }
}
