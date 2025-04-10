

using System.ComponentModel.DataAnnotations;

namespace PatientManager.Persistance.Models.atencionmedica
{
    public class CitasModel
    {
        public int CitaID { get; set; }

        [Display(Name ="Nombre del paciente")]
        [Required]
        public int PacienteID { get; set; }

        [Display(Name ="Nombre del medico")]
        [Required]
        public int MedicoID { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(7, ErrorMessage = "La hora no puede superar los 7 caracteres.")]
        public TimeSpan Hora { get; set; }

        [Required]
        public string Causa { get; set; }

        [Required]
        public int EstadoID { get; set; }

        [Required]
        public int ConsultorioID { get; set; }

    }
}
