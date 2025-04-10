using Microsoft.AspNetCore.Mvc.Rendering;
using PatientManager.Application.Contracts.administracion;
using PatientManager.Persistance.Models.administracion;

namespace PatientManager.Web.HelpersWeb.administracion.usuario
{
    public class RegisterHelper
    {

        public async Task<List<SelectListItem>> GetUserType()
        {
            return await Task.FromResult(new List<SelectListItem>
            {
                new SelectListItem { Text = "Administrador", Value = "Administrador" }
            });
        }

    }
}
