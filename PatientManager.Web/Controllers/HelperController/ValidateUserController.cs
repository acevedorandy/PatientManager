using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PatientManager.Web.HelpersWeb.administracion;

namespace PatientManager.Web.Controllers.HelperController
{
    public class ValidateUserController : IActionFilter
    {
        private readonly ValidateUserSesion _validateUserSesion;

        public ValidateUserController(ValidateUserSesion validateUserSesion)
        {
            _validateUserSesion = validateUserSesion;
        }

        // Filtro para aplicar la logica de ValidateUserSesion globalmente y para excluirlo por igual
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var ignoreFilter = context.ActionDescriptor.EndpointMetadata
                .Any(metadata => metadata is IgnoreSessionValidation);

            if (ignoreFilter)
            {
                return;
            }

            if (!_validateUserSesion.HasUser())
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { Controller = "Login", Action = "Index" }));
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
