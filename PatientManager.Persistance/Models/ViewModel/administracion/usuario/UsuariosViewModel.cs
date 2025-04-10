

using System.ComponentModel.DataAnnotations;

namespace PatientManager.Persistance.Models.ViewModel.administracion.usuario
{
    public class UsuariosViewModel
    {
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string TipoUsuario { get; set; }
        public int ConsultorioID { get; set; }
    }
}
