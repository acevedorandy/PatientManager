

using PatientManager.Domain.Entities.administracion;
using PatientManager.Domain.Result;
using PatientManager.Persistance.Validations.IBaseValidations;

namespace PatientManager.Persistance.Validations.administracion
{
    public class EstadosValidations : IValidations<Estados>
    {
        public OperationResult ValidateSave(Estados estados)
        {
            OperationResult result = new OperationResult();

            if (estados == null)
            {
                result.Success = false;
                result.Message = "El estado es requerido.";
                return result;
            }
            if (string.IsNullOrEmpty(estados.Nombre) || (estados.Nombre.Length > 100))
            {
                result.Success = false;
                result.Message = "El nombre es requerido y debe de ser menor a 100 caracteres.";
                return result;
            }
            return result;
        }

        public OperationResult ValidateUpdate(Estados estados)
        {
            OperationResult result = new OperationResult();

            if (estados == null)
            {
                result.Success = false;
                result.Message = "El estado es requerido.";
                return result;
            }
            if (estados.EstadoID <= 0)
            {
                result.Success = false;
                result.Message = "El ID del estado es requerido.";
                return result;
            }
            if (string.IsNullOrEmpty(estados.Nombre) || (estados.Nombre.Length > 100))
            {
                result.Success = false;
                result.Message = "El nombre es requerido y debe de ser menor a 100 caracteres.";
                return result;
            }
            return result;
        }

        public OperationResult ValidateRemove(Estados estados)
        {
            OperationResult result = new OperationResult();

            if (estados == null)
            {
                result.Success = false;
                result.Message = "El estado es requerido.";
                return result;
            }
            if (estados.EstadoID <= 0)
            {
                result.Success = false;
                result.Message = "El ID del estado es requerido.";
                return result;
            }
            if (string.IsNullOrEmpty(estados.Nombre) || (estados.Nombre.Length > 100))
            {
                result.Success = false;
                result.Message = "El nombre es requerido y debe de ser menor a 100 caracteres.";
                return result;
            }
            return result;
        }


    }
}
