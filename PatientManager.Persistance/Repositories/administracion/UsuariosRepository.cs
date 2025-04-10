
using PatientManager.Domain.Entities.administracion;
using PatientManager.Domain.Result;
using PatientManager.Persistance.Base;
using PatientManager.Persistance.Context;
using PatientManager.Persistance.Interfaces.administracion;
using PatientManager.Persistance.Models.administracion;
using PatientManager.Persistance.Validations.administracion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PatientManager.Persistance.HelpersRepository.administracion;
using PatientManager.Persistance.Models.ViewModel.administracion.usuario;

namespace PatientManager.Persistance.Repositories.administracion
{
    public sealed class UsuariosRepository(PatientManagerContext patientManagerContext,
                 ILogger<UsuariosRepository> logger, UsuariosValidations usuariosValidations) : BaseRepository<Usuarios>(patientManagerContext), IUsuariosRepository
    {
        private readonly PatientManagerContext _patientManagerContext = patientManagerContext;
        private readonly ILogger<UsuariosRepository> _logger = logger;
        private readonly UsuariosValidations _usuariosValidations = usuariosValidations;

        public async override Task<OperationResult> Save(Usuarios usuarios)
        {
            OperationResult result = new OperationResult();

            _usuariosValidations.ValidateSave(usuarios);

            try
            {
                bool usuarioExiste = await _patientManagerContext.Usuarios.AnyAsync(u => u.NombreUsuario == usuarios.NombreUsuario);
                bool correoExiste = await _patientManagerContext.Usuarios.AnyAsync(u => u.Correo == usuarios.Correo);
                if (usuarioExiste)
                {
                    result.Success = false;
                    result.Message = "El nombre de usuario ya está en uso. Por favor, elija otro.";
                    return result;
                }

                usuarios.Contraseña = PasswordEncryption.ComputeSha256Hash(usuarios.Contraseña);
                result = await base.Save(usuarios);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error guardando el usuario.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }


        public async override Task<OperationResult> Update(Usuarios usuarios)
        {
            OperationResult result = new OperationResult();

            _usuariosValidations.ValidateUpdate(usuarios);

            try
            {
                Usuarios? usuariosToUpdate = await _patientManagerContext.Usuarios.FindAsync(usuarios.UsuarioID);

                usuariosToUpdate.UsuarioID = usuarios.UsuarioID;
                usuariosToUpdate.Nombre = usuarios.Nombre;
                usuariosToUpdate.Apellido = usuarios.Apellido;
                usuariosToUpdate.Correo = usuarios.Correo;
                usuariosToUpdate.NombreUsuario = usuarios.NombreUsuario;
                usuariosToUpdate.Contraseña = usuarios.Contraseña = PasswordEncryption.ComputeSha256Hash(usuarios.Contraseña);
                usuariosToUpdate.TipoUsuario = usuarios.TipoUsuario;
                usuariosToUpdate.ConsultorioID = usuarios.ConsultorioID;

                result = await base.Update(usuariosToUpdate);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error actualizando el usuario.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> Remove(Usuarios usuarios)
        {
            OperationResult result = new OperationResult();

            _usuariosValidations.ValidateRemove(usuarios);

            try
            {
                result = await base.Remove(usuarios);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error eliminando el usuario.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from administracion in _patientManagerContext.Usuarios
                                     orderby administracion.UsuarioID descending

                                     select new UsuariosModel()
                                     {
                                         UsuarioID = administracion.UsuarioID,
                                         Nombre = administracion.Nombre,
                                         Apellido = administracion.Apellido,
                                         Correo = administracion.Correo,
                                         NombreUsuario = administracion.NombreUsuario,
                                         Contraseña = administracion.Contraseña,
                                         TipoUsuario = administracion.TipoUsuario,
                                         ConsultorioID = administracion.ConsultorioID

                                     }).AsNoTracking()
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo los usuarios.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async override Task<OperationResult> GetById(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                result.Data = await (from administracion in _patientManagerContext.Usuarios
                                     where administracion.UsuarioID == id

                                     select new UsuariosModel()
                                     {
                                         UsuarioID = administracion.UsuarioID,
                                         Nombre = administracion.Nombre,
                                         Apellido = administracion.Apellido,
                                         Correo = administracion.Correo,
                                         NombreUsuario = administracion.NombreUsuario,
                                         Contraseña = administracion.Contraseña,
                                         TipoUsuario = administracion.TipoUsuario,
                                         ConsultorioID = administracion.ConsultorioID

                                     }).AsNoTracking()
                                     .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo el usuario.";
                _logger.LogError(result.Message, ex.ToString());
            }
            return (result);
        }

        public async Task<Usuarios> Login(LogInUserViewModel logInUser)
        {
            try
            {
                string passwordEnd = PasswordEncryption.ComputeSha256Hash(logInUser.Contraseña);

                Usuarios usuarios = await _patientManagerContext.Set<Usuarios>()
                    .FirstOrDefaultAsync(usuarios => usuarios.NombreUsuario == logInUser.NombreUsuario && usuarios.Contraseña == passwordEnd);

                return usuarios;
            }
            catch (Exception ex)
            {
                _logger.LogError("Ha ocurrido un error iniciando sesión: {Error}", ex.ToString());
                return null;
            }
        }

    }
}
