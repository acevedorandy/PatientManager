

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PatientManager.Domain.Entities.laboratorio
{
    [Table("ResultadosLaboratorio", Schema = "laboratorio")]

    public class ResultadosLaboratorio
    {
        [Key]
        public int ResultadoID { get; set; }
        public int PruebaID { get; set; }
        public string? Resultado { get; set; }
        public int EstadoID { get; set; }
        public int PacienteID { get; set; }
        public int ConsultorioID { get; set; }

    }
}
