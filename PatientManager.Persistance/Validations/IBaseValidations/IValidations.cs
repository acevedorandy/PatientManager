using PatientManager.Domain.Result;

namespace PatientManager.Persistance.Validations.IBaseValidations
{
    public interface IValidations<T>
    {
        OperationResult ValidateSave(T entity);
        OperationResult ValidateUpdate(T entity);
        OperationResult ValidateRemove(T entity);
    }
}
