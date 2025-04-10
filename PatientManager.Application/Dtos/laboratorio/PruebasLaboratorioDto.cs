

using System.ComponentModel.DataAnnotations;

namespace PatientManager.Application.Dtos.laboratorio
{
    public class PruebasLaboratorioDto
    {
        public int PruebaID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        [Display(Name = "Nombre de la prueba")]
        public string NombrePrueba { get; set; }

        [Display(Name = "Consultorio")]
        [Required]
        public int ConsultorioID { get; set; }

    }
}
