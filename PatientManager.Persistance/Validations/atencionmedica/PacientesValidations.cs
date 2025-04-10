

using PatientManager.Domain.Entities.atencionmedica;
using PatientManager.Domain.Result;
using PatientManager.Persistance.Validations.IBaseValidations;

namespace PatientManager.Persistance.Validations.atencionmedica
{
    public class PacientesValidations : IValidations<Pacientes>
    {
        public OperationResult ValidateSave(Pacientes pacientes)
        {
            OperationResult result = new OperationResult();

            if (pacientes == null)
            {
                result.Success = false;
                result.Message = "El paciente es requerido.";
                return result;
            }
            if (string.IsNullOrEmpty(pacientes.Nombre) || 
                string.IsNullOrEmpty(pacientes.Apellido) ||
                pacientes.Nombre.Length > 100 ||
                pacientes.Apellido.Length > 100)
            {
                result.Success = false;
                result.Message = "El nombre y apellido del paciente son requeridos y debe ser menor a 100 caracteres.";
                return result;
            }
            if (string.IsNullOrEmpty(pacientes.Telefono) || pacientes.Telefono.Length > 20)
            {
                result.Success = false;
                result.Message = "El telefono es requerido y debe ser menor a 20 caracteres.";
                return result;
            }
            if (string.IsNullOrEmpty(pacientes.Direccion) || pacientes.Direccion.Length > 255)
            {
                result.Success = false;
                result.Message = "La direccion es requerida y debe ser menor a 255 caracteres.";
                return result;
            }
            if (string.IsNullOrEmpty(pacientes.Cedula) || pacientes.Cedula.Length > 11)
            {
                result.Success = false;
                result.Message = "La cedula es requerida y debe ser menor a 11 caracteres.";
                return result;
            }
            if (pacientes.FechaNacimiento == null || pacientes.Fumador == null)
            {
                result.Success = false;
                result.Message = "Es requerido.";
            }
            return result;
        }

        public OperationResult ValidateUpdate(Pacientes pacientes)
        {
            OperationResult result = new OperationResult();

            if (pacientes == null)
            {
                result.Success = false;
                result.Message = "El paciente es requerido.";
                return result;
            }
            if (pacientes.PacienteID < 0 || pacientes.ConsultorioID < 0)
            {
                result.Success = false;
                result.Message = "El ID del paciente y el consultorio son requerido.";
                return result;
            }
            if (string.IsNullOrEmpty(pacientes.Nombre) ||
                string.IsNullOrEmpty(pacientes.Apellido) ||
                pacientes.Nombre.Length > 100 ||
                pacientes.Apellido.Length > 100)
            {
                result.Success = false;
                result.Message = "El nombre y apellido del paciente son requeridos y debe ser menor a 100 caracteres.";
                return result;
            }
            if (string.IsNullOrEmpty(pacientes.Telefono) || pacientes.Telefono.Length > 20)
            {
                result.Success = false;
                result.Message = "El telefono es requerido y debe ser menor a 20 caracteres.";
                return result;
            }
            if (string.IsNullOrEmpty(pacientes.Direccion) || pacientes.Direccion.Length > 255)
            {
                result.Success = false;
                result.Message = "La direccion es requerida y debe ser menor a 255 caracteres.";
                return result;
            }
            if (string.IsNullOrEmpty(pacientes.Cedula) || pacientes.Cedula.Length > 11)
            {
                result.Success = false;
                result.Message = "La cedula es requerida y debe ser menor a 11 caracteres.";
                return result;
            }
            if (pacientes.FechaNacimiento == null || pacientes.Fumador == null)
            {
                result.Success = false;
                result.Message = "Es requerido.";
            }
            return result;
        }

        public OperationResult ValidateRemove(Pacientes pacientes)
        {
            OperationResult result = new OperationResult();

            if (pacientes == null)
            {
                result.Success = false;
                result.Message = "El paciente es requerido.";
                return result;
            }
            if (pacientes.PacienteID < 0)
            {
                result.Success = false;
                result.Message = "El ID del paciente es requerido.";
                return result;
            }
            return result;
        }
    }
}
