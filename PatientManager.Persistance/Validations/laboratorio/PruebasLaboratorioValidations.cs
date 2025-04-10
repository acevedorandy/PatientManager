

using PatientManager.Domain.Entities.laboratorio;
using PatientManager.Domain.Result;
using PatientManager.Persistance.Validations.IBaseValidations;

namespace PatientManager.Persistance.Validations.laboratorio
{
    public class PruebasLaboratorioValidations : IValidations<PruebasLaboratorio>
    {
        public OperationResult ValidateSave(PruebasLaboratorio pruebas)
        {
            OperationResult result = new OperationResult();

            if (pruebas == null)
            {
                result.Success = false;
                result.Message = "La prueba es requerida.";
                return result;
            }
            if (pruebas.ConsultorioID <0)
            {
                result.Success = false;
                result.Message = "El ID del consultorio es requerido.";
                return result;
            }
            if (string.IsNullOrEmpty(pruebas.NombrePrueba) || pruebas.NombrePrueba.Length > 100)
            {
                result.Success = false;
                result.Message = "El nombre de la prueba es requerido y debe ser menor a 100 caracteres.";
                return result;
            }
            return result;
        }

        public OperationResult ValidateUpdate(PruebasLaboratorio pruebas)
        {
            OperationResult result = new OperationResult();

            if (pruebas == null)
            {
                result.Success = false;
                result.Message = "La prueba es requerida.";
                return result;
            }
            if (pruebas.PruebaID < 0)
            {
                result.Success = false;
                result.Message = "El ID de la prueba es requerido.";
                return result;
            }
            if (string.IsNullOrEmpty(pruebas.NombrePrueba) || pruebas.NombrePrueba.Length > 100)
            {
                result.Success = false;
                result.Message = "El nombre de la prueba es requerido y debe ser menor a 100 caracteres.";
                return result;
            }
            return result;
        }

        public OperationResult ValidateRemove(PruebasLaboratorio pruebas)
        {
            OperationResult result = new OperationResult();

            if (pruebas == null)
            {
                result.Success = false;
                result.Message = "La prueba es requerida.";
                return result;
            }
            if (pruebas.PruebaID < 0)
            {
                result.Success = false;
                result.Message = "El ID de la prueba es requerido.";
                return result;
            }
            return result;
        }
    }
}
