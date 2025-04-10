

using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace PatientManager.Persistance.Models.administracion
{
    public class UsuariosModel
    {
        [Required]
        public int UsuarioID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El apellido no puede superar los 100 caracteres.")]
        public string Apellido { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El correo no puede superar los 100 caracteres.")]
        public string Correo { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
        public string NombreUsuario { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "El nombre no puede superar los 255 caracteres.")]
        public string Contraseña { get; set; }

        [Display(Name = "Confirmar contraseña")]

        [Compare(nameof(Contraseña), ErrorMessage = "La Contraseña deben coincidir.")]

        [DataType(DataType.Password)]
        public string ConfirmarContraseña { get; set; }

        [Display(Name = "Tipo de usuario")]
        [Required]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede superar los 50 caracteres.")]
        public string TipoUsuario { get; set; }

        [Required]
        public int ConsultorioID { get; set; }

    }
}
