

using System.ComponentModel.DataAnnotations;

namespace PatientManager.Persistance.Models.ViewModel.administracion.usuario
{
    public class SaveUserViewModel
    {
        public int UsuarioID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "El apellido no puede superar los 100 caracteres.")]
        [DataType(DataType.Text)]
        public string Apellido { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "El correo no puede superar los 100 caracteres.")]
        public string Correo { get; set; }


        [Display(Name = "Nombre de usuario")]
        [Required]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede superar los 50 caracteres.")]
        public string NombreUsuario { get; set; }


        [Required]
        [StringLength(255, ErrorMessage = "La contraseña no puede superar los 100 caracteres.")]
        public string Contraseña { get; set; }

        [Display(Name = "Confirmar contraseña")]
        [Compare(nameof(Contraseña), ErrorMessage = "La Contraseña deben coincidir.")]
        [DataType(DataType.Password)]
        public string ConfirmarContraseña { get; set; }

        [Display(Name = "Tipo de usuario")]
        [Required(ErrorMessage = "El tipo de usuario es requerido.")]
        public string TipoUsuario { get; set; }


        [Display(Name = "Consultorio")]
        public string NombreConsultorio { get; set; }

    }
}
