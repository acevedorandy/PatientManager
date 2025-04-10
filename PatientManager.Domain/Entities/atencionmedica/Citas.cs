

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PatientManager.Domain.Entities.atencionmedica
{
    [Table("Citas", Schema = "atencionmedica")]

    public class Citas
    {
        [Key]
        public int CitaID { get; set; }
        public int PacienteID { get; set; }
        public int MedicoID { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string Causa { get; set; }
        public int EstadoID { get; set; }
        public int ConsultorioID { get; set; }

    }
}
