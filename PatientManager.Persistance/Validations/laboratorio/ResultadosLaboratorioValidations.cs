

using PatientManager.Domain.Entities.laboratorio;
using PatientManager.Domain.Result;
using PatientManager.Persistance.Validations.IBaseValidations;

namespace PatientManager.Persistance.Validations.laboratorio
{
    public class ResultadosLaboratorioValidations : IValidations<ResultadosLaboratorio>
    {
        public OperationResult ValidateSave(ResultadosLaboratorio resultados)
        {
            OperationResult result = new OperationResult();

            if (resultados == null)
            {
                result.Success = false;
                result.Message = "El resultado es requerido.";
                return result;
            }
            if (resultados.PruebaID < 0 || resultados.EstadoID < 0 || resultados.PacienteID < 0 || resultados.ConsultorioID < 0)
            {
                result.Success = false;
                result.Message = "El ID de la prueba, el estado, paciente y el consultorio son requeridos.";
                return result;
            }
            return result;
        }

        public OperationResult ValidateUpdate(ResultadosLaboratorio resultados)
        {
            OperationResult result = new OperationResult();

            if (resultados == null)
            {
                result.Success = false;
                result.Message = "El resultado es requerido.";
                return result;
            }
            if (resultados.ResultadoID < 0 ||resultados.PruebaID < 0 || resultados.EstadoID < 0 || resultados.PacienteID < 0)
            {
                result.Success = false;
                result.Message = "El ID del resultado, la prueba, el estado y el paciente son requeridos.";
                return result;
            }
            if (resultados.Resultado == null)
            {
                result.Success = false;
                result.Message = "El resultado es requerido.";
                return result;
            }
            return result;
        }

        public OperationResult ValidateRemove(ResultadosLaboratorio resultados)
        {
            OperationResult result = new OperationResult();

            if (resultados == null)
            {
                result.Success = false;
                result.Message = " El resultado es requerido.";
                return result;
            }
            if (resultados.ResultadoID < 0)
            {
                result.Success = false;
                result.Message = "El ID del resultado es requerido.";
                return result;
            }
            return result;
        }


    }
}
