

using System.ComponentModel.DataAnnotations;

namespace PatientManager.Application.Dtos.administracion
{
    public class UsuariosDto
    {
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }


        [Display(Name ="Nombre de usuario")]
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }

        [Display(Name = "Confirmar contraseña")]
        [Compare(nameof(Contraseña), ErrorMessage = "La Contraseña debe coincidir.")]
        [DataType(DataType.Password)]
        public string ConfirmarContraseña { get; set; }

        [Display(Name = "Tipo de usuario")]
        public string TipoUsuario { get; set; }
        public int ConsultorioID { get; set; }

    }
}
