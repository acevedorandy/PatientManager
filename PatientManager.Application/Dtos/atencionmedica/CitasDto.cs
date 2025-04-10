
using System.ComponentModel.DataAnnotations;

namespace PatientManager.Application.Dtos.atencionmedica
{
    public class CitasDto
    {
        public int CitaID { get; set; }

        [Required]
        public int PacienteID { get; set; }

        [Required]
        public int MedicoID { get; set; }
        
        [Required]
        public DateTime Fecha { get; set; }
        
        [Required]
        public TimeSpan Hora { get; set; }

        public string Causa { get; set; }

        public int EstadoID { get; set; }

        [Required]
        public int ConsultorioID { get; set; }

    }
}
