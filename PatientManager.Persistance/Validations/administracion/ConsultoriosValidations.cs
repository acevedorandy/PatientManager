

using PatientManager.Domain.Entities.administracion;
using PatientManager.Domain.Result;
using PatientManager.Persistance.Validations.IBaseValidations;

namespace PatientManager.Persistance.Validations.administracion
{
    public class ConsultoriosValidations : IValidations<Consultorios>
    {
        public OperationResult ValidateSave(Consultorios consultorios)
        {
            OperationResult result = new OperationResult();

            if (consultorios == null)
            {
                result.Success = false;
                result.Message = "El ID del consultorio es requerido.";
                return result;
            }
            if (string.IsNullOrEmpty(consultorios.NombreConsultorio) || consultorios.NombreConsultorio.Length > 100)
            {
                result.Success = false;
                result.Message = "El nombre es requerido y debe ser menor a 100 caracteres.";
                return result;
            }
            return result;
        }

        public OperationResult ValidateUpdate(Consultorios consultorios)
        {
            OperationResult result = new OperationResult();

            if (consultorios == null)
            {
                result.Success = false;
                result.Message = "El consultorio es requerido.";
                return result;
            }
            if (consultorios.ConsultorioID <= 0)
            {
                result.Success = false;
                result.Message = "El ID del consultorio es requerido.";
                return result;
            }
            if (string.IsNullOrEmpty(consultorios.NombreConsultorio) || consultorios.NombreConsultorio.Length > 100)
            {
                result.Success = false;
                result.Message = "El nombre es requerido y debe ser menor a 100 caracteres.";
                return result;
            }
            return result;
        }

        public OperationResult ValidateRemove(Consultorios consultorios)
        {
            OperationResult result = new OperationResult();

            if (consultorios == null)
            {
                result.Success = false;
                result.Message = "El ID del consultorio es requerido.";
                return result;
            }
            if (consultorios.ConsultorioID <= 0)
            {
                result.Success = false;
                result.Message = "El ID del consultorio es requerido.";
                return result;
            }
            return result;
        }




    }
}
