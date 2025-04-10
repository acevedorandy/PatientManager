

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PatientManager.Domain.Entities.atencionmedica
{
    [Table("Medicos", Schema = "atencionmedica")]

    public class Medicos
    {
        [Key]
        public int MedicoID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Cedula { get; set; }
        public string? Foto { get; set; }
        public int ConsultorioID { get; set; }
        
    }
}
