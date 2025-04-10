
using System.ComponentModel.DataAnnotations;

namespace PatientManager.Persistance.Models.laboratorio
{
    public class PruebasLaboratorioModel
    {
        [Required]
        public int PruebaID { get; set; }

        [Display(Name ="Nombre de la prueba")]
        [Required]
        [StringLength(100, ErrorMessage = "El nombre de la prueba no puede superar los 100 caracteres.")]
        public string NombrePrueba { get; set; }

        [Required]
        public int ConsultorioID { get; set; }

    }
}
