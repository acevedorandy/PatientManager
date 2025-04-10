

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PatientManager.Domain.Entities.administracion
{
    [Table("Estados", Schema = "administracion")]

    public class Estados
    {
        [Key]
        public int EstadoID { get; set; }
        public string Nombre { get; set; }

    }
}
