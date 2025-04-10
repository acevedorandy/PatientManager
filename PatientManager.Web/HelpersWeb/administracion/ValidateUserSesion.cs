using PatientManager.Application.Helpers.web;
using PatientManager.Persistance.Models.ViewModel.administracion.usuario;

namespace PatientManager.Web.HelpersWeb.administracion
{
    public class ValidateUserSesion
    {
        private readonly IHttpContextAccessor _httpContextAccessor1;

        public ValidateUserSesion(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor1 = httpContextAccessor;
        }

        public bool HasUser()
        {
            UsuariosViewModel usuariosViewModel = _httpContextAccessor1.HttpContext.Session.Get<UsuariosViewModel>("usuario");

            if (usuariosViewModel == null)
            {
                return false;
            }
            else 
            {
                return true;
            }
        }
    }
}
