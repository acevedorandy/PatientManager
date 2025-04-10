

using System.ComponentModel.DataAnnotations;

namespace PatientManager.Application.Dtos.laboratorio
{
    public class ResultadosLaboratorioDto
    {
        public int ResultadoID { get; set; }

        [Required]
        public int PruebaID { get; set; }

        public string? Resultado { get; set; }

        [Required]
        public int EstadoID { get; set; }

        [Required]
        public int PacienteID { get; set; }

        [Required]
        public int ConsultorioID { get; set; }

    }
}
