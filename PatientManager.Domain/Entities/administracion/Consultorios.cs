

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PatientManager.Domain.Entities.administracion
{
    [Table("Consultorios", Schema = "administracion")]
    public class Consultorios
    {
        [Key]
        public int ConsultorioID { get; set; }
        public string NombreConsultorio { get; set; }

    }
}
