using PatientManager.Domain.Entities.administracion;
using PatientManager.Domain.Result;
using PatientManager.Persistance.Validations.IBaseValidations;

namespace PatientManager.Persistance.Validations.administracion
{
    public class UsuariosValidations : IValidations<Usuarios>
    {
        public OperationResult ValidateSave(Usuarios usuarios)
        {
            OperationResult result = new OperationResult();

            if (usuarios == null)
            {
                result.Success = false;
                result.Message = "El ID del consultorio es requerido.";
                return result;
            }
            if (string.IsNullOrEmpty(usuarios.Nombre) ||
                string.IsNullOrEmpty(usuarios.Apellido) ||
                string.IsNullOrEmpty(usuarios.Correo) ||
                usuarios.Nombre.Length > 100 ||
                usuarios.Apellido.Length > 100 ||
                usuarios.Correo.Length > 100)
            {
                result.Success = false;
                result.Message = "El nombre, apellido y correo son requeridos y deben ser menores a 100 caracteres.";
                return result;
            }
            if (string.IsNullOrEmpty(usuarios.NombreUsuario) || usuarios.NombreUsuario.Length > 50)
            {
                result.Success = false;
                result.Message = "El nombre de usuario es requerido y debe ser menor a 50 caracteres.";
                return result;
            }
            if (string.IsNullOrEmpty(usuarios.Contraseña) || usuarios.Contraseña.Length > 255)
            {
                result.Success = false;
                result.Message = "El nombre de usuario es requerido y debe ser menor a 50 caracteres.";
                return result;
            }
            if (string.IsNullOrEmpty(usuarios.TipoUsuario) || usuarios.TipoUsuario.Length > 50)
            {
                result.Success = false;
                result.Message = "El tipo de usuario es requerido y debe ser menor a 50 caracteres.";
                return result;
            }
            if (usuarios.TipoUsuario != "Asistente" && usuarios.TipoUsuario != "Administrador")
            {
                result.Success = false;
                result.Message = "TipoUsuario inválido. Solo se permiten 'Asistente' o 'Administrador'.";
                return result;
            }
            if (usuarios.ConsultorioID <= 0 )
            {
                result.Success = false;
                result.Message = "El ID del consultorio es requerido.";
                return result;
            }
            return result;
        }

        public OperationResult ValidateUpdate(Usuarios usuarios)
        {
            OperationResult result = new OperationResult();

            if (usuarios == null)
            {
                result.Success = false;
                result.Message = "El ID del consultorio es requerido.";
                return result;
            }
            if (usuarios.UsuarioID <= 0)
            {
                result.Success = false;
                result.Message = "El ID del usuario es requerido.";
                return result;
            }
            if (string.IsNullOrEmpty(usuarios.Nombre) ||
                string.IsNullOrEmpty(usuarios.Apellido) ||
                string.IsNullOrEmpty(usuarios.Correo) ||
                usuarios.Nombre.Length > 100 ||
                usuarios.Apellido.Length > 100 ||
                usuarios.Correo.Length > 100)
            {
                result.Success = false;
                result.Message = "El nombre, apellido y correo son requeridos y deben ser menores a 100 caracteres.";
                return result;
            }
            if (string.IsNullOrEmpty(usuarios.NombreUsuario) || usuarios.NombreUsuario.Length > 50)
            {
                result.Success = false;
                result.Message = "El nombre de usuario es requerido y debe ser menor a 50 caracteres.";
                return result;
            }
            if (string.IsNullOrEmpty(usuarios.Contraseña) || usuarios.Contraseña.Length > 255)
            {
                result.Success = false;
                result.Message = "El nombre de usuario es requerido y debe ser menor a 50 caracteres.";
                return result;
            }
            if (string.IsNullOrEmpty(usuarios.TipoUsuario) || usuarios.TipoUsuario.Length > 50)
            {
                result.Success = false;
                result.Message = "El tipo de usuario es requerido y debe ser menor a 50 caracteres.";
                return result;
            }
            if (usuarios.ConsultorioID <= 0)
            {
                result.Success = false;
                result.Message = "El ID del consultorio es requerido.";
                return result;
            }
            return result;
        }
        public OperationResult ValidateRemove(Usuarios usuarios)
        {
            OperationResult result = new OperationResult();

            if (usuarios == null)
            {
                result.Success = false;
                result.Message = "El ID del consultorio es requerido.";
                return result;
            }
            if (usuarios.UsuarioID <= 0)
            {
                result.Success = false;
                result.Message = "El ID del usuario es requerido.";
                return result;
            }
            return result;
        }


    }
}
