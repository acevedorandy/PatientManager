using PatientManager.Domain.Entities.atencionmedica;
using PatientManager.Domain.Result;
using PatientManager.Persistance.Validations.IBaseValidations;


namespace PatientManager.Persistance.Validations.atencionmedica
{
    public class MedicosValidations : IValidations<Medicos>
    {
        public OperationResult ValidateSave(Medicos medicos)
        {
            OperationResult result = new OperationResult();

            if (medicos == null)
            {
                result.Success = false;
                result.Message = "El medico es requerido.";
                return result;
            }
            if (string.IsNullOrEmpty(medicos.Nombre) ||
                string.IsNullOrEmpty(medicos.Apellido) ||
                string.IsNullOrEmpty(medicos.Correo) ||
                medicos.Nombre.Length > 100 ||
                medicos.Apellido.Length > 100 ||
                medicos.Correo.Length > 100)
            {
                result.Success = false;
                result.Message = "El nombre, apellido y correo son requeridos y deben ser menor a 100 caracteres.";
                return result;
            }
            if (string.IsNullOrEmpty(medicos.Telefono) || medicos.Telefono.Length > 20)
            {
                result.Success = false;
                result.Message = "El telefono es requerido y debe ser menor a 20 caracteres.";
                return result;
            }
            if (string.IsNullOrEmpty(medicos.Cedula) || medicos.Cedula.Length > 11)
            {
                result.Success = false;
                result.Message = "La cedula es requerida y debe ser menor a 11 caracteres.";
                return result;
            }
            if (medicos.ConsultorioID < 0)
            {
                result.Success = false;
                result.Message = "El ID del consultorio es requerido.";
                return result;
            }
            return result;
        }
        public OperationResult ValidateUpdate(Medicos medicos)
        {
            OperationResult result = new OperationResult(); 

            if (medicos == null)
            {
                result.Success = false;
                result.Message = "El medico es requerido.";
                return result;
            }
            if (string.IsNullOrEmpty(medicos.Nombre) ||
                string.IsNullOrEmpty(medicos.Apellido) ||
                string.IsNullOrEmpty(medicos.Correo) ||
                medicos.Nombre.Length > 100 ||
                medicos.Apellido.Length > 100 ||
                medicos.Correo.Length > 100)
            {
                result.Success = false;
                result.Message = "El nombre, apellido y correo son requeridos y deben ser menor a 100 caracteres.";
                return result;
            }
            if (string.IsNullOrEmpty(medicos.Telefono) || medicos.Telefono.Length > 20)
            {
                result.Success = false;
                result.Message = "El telefono es requerido y debe ser menor a 20 caracteres.";
                return result;
            }
            if (string.IsNullOrEmpty(medicos.Cedula) || medicos.Cedula.Length > 11)
            {
                result.Success = false;
                result.Message = "La cedula es requerida y debe ser menor a 11 caracteres.";
                return result;
            }
            if (medicos.MedicoID < 0 || medicos.ConsultorioID < 0 )
            {
                result.Success = false;
                result.Message = "El ID del medico, consultorio y son requeridos.";
                return result;
            }
            return result;
        }
        public OperationResult ValidateRemove(Medicos medicos)
        {
            OperationResult result = new OperationResult();

            if (medicos != null)
            {
                result.Success = false;
                result.Message = "El medico es requerido.";
                return result;
            }
            if (medicos.MedicoID < 0 )
            {
                result.Success = false;
                result.Message = "El ID del medico es requerido.";
                return result;
            }
            return result;
        }


    }
}
