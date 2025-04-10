

using System.ComponentModel.DataAnnotations;

namespace PatientManager.Persistance.Models.laboratorio
{
    public class ResultadosLaboratorioModel
    {
        [Required]
        public int ResultadoID { get; set; }

        [Required]
        public int PruebaID { get; set; }

        [Required]
        public string? Resultado { get; set; }

        [Required]
        public int EstadoID { get; set; }

        [Required]
        public int PacienteID { get; set; }

        [Required]
        public int ConsultorioID { get; set; }

    }
}
