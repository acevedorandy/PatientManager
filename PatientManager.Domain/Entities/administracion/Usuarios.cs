
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PatientManager.Domain.Entities.administracion
{
    [Table("Usuarios", Schema = "administracion")]

    public class Usuarios
    {
        [Key]
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
