
using System.ComponentModel.DataAnnotations;

namespace PatientManager.Persistance.Models.ViewModel.administracion.usuario
{
    public class LogInUserViewModel
    {
        [Display(Name = "Nombre de usuario")]
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }

    }
}
