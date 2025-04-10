

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PatientManager.Domain.Entities.laboratorio
{
    [Table("PruebasLaboratorio", Schema = "laboratorio")]

    public class PruebasLaboratorio
    {
        [Key]
        public int PruebaID { get; set; }
        public string NombrePrueba { get; set; }
        public int ConsultorioID { get; set; }

    }
}
