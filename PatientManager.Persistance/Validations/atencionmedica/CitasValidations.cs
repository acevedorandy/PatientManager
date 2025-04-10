

using PatientManager.Domain.Entities.atencionmedica;
using PatientManager.Domain.Result;
using PatientManager.Persistance.Validations.IBaseValidations;

namespace PatientManager.Persistance.Validations.atencionmedica
{
    public class CitasValidations : IValidations<Citas>
    {
        public OperationResult ValidateSave(Citas citas)
        {
            OperationResult result = new OperationResult();

            if (citas == null)
            {
                result.Success = false;
                result.Message = "La cita es requerida.";
                return result;
            }
            if (citas.PacienteID <= 0 || citas.MedicoID <= 0 || citas.EstadoID <= 0 || citas.ConsultorioID < 0)
            {
                result.Success = false;
                result.Message = "El ID de la cita, paciente, médico, estado y consultorio son requeridos.";
                return result;
            }
            if (citas.Fecha == null || citas.Hora == null)
            {
                result.Success = false;
                result.Message = "La fecha y hora son requeridas.";
                return result;
            }
            return result;
        }

        public OperationResult ValidateUpdate(Citas citas)
        {
            OperationResult result = new OperationResult();

            if (citas == null)
            {
                result.Success = false;
                result.Message = "La cita es requerida.";
                return result;
            }
            if (citas.CitaID <= 0 || citas.PacienteID <= 0 || citas.MedicoID <= 0 || citas.EstadoID <= 0)
            {
                result.Success = false;
                result.Message = "El ID de la cita, paciente, médico y el estado son requeridos.";
                return result;
            }
            if (citas.Fecha == null || citas.Hora == null)
            {
                result.Success = false;
                result.Message = "La fecha y hora son requeridas.";
                return result;
            }
            return result;
        }

        public OperationResult ValidateRemove(Citas citas)
        {
            OperationResult result = new OperationResult();

            if (citas == null)
            {
                result.Success = false;
                result.Message = "La cita es requerida.";
                return result;
            }
            if (citas.PacienteID <= 0)
            {
                result.Success = false;
                result.Message = "El ID de la cita es requerida.";
                return result;
            }
            return result;
        }


    }
}
