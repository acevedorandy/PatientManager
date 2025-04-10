
namespace PatientManager.Web.Controllers.HelperController
{
    // Atributo para ignorar el filtro
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class IgnoreSessionValidation : Attribute
    {
    }
}
