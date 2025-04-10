using Microsoft.AspNetCore.Mvc.Rendering;
using PatientManager.Application.Contracts.administracion;
using PatientManager.Persistance.Models.administracion;

namespace PatientManager.Web.HelpersWeb.administracion.usuario
{
    public class RegisterHelper
    {
        private readonly IConsultoriosServices _consultoriosServices;

        public RegisterHelper(IConsultoriosServices consultoriosServices)
        {
            _consultoriosServices = consultoriosServices;
        }

        public async Task<List<SelectListItem>> GetConsultorio()
        {
            var consultorioList = new List<SelectListItem>();
            var result = await _consultoriosServices.GetAll();

            if (result.IsSuccess)
            {
                var consultorios = (IEnumerable<ConsultoriosModel>)result.Model;
                consultorioList.AddRange(consultorios.Select(x => new SelectListItem
                {
                    Text = x.Nombre,
                    Value = x.ConsultorioID.ToString()
                }));
            }
            return consultorioList;
        }

        public async Task<List<SelectListItem>> GetUserType()
        {
            return await Task.FromResult(new List<SelectListItem>
            {
                new SelectListItem { Text = "Administrador", Value = "Administrador" },
                new SelectListItem { Text = "Asistente", Value = "Asistente" }
            });
        }

    }
}
