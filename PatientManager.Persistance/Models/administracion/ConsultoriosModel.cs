

using System.ComponentModel.DataAnnotations;

namespace PatientManager.Persistance.Models.administracion
{
    public class ConsultoriosModel
    {
        [Required]
        public int ConsultorioID { get; set; }

        [Display(Name ="Nombre del consultorio")]
        [Required]
        [StringLength(100, ErrorMessage = "El nombre del consultorio no puede superar los 100 caracteres.")]
        public string NombreConsultorio { get; set; }

    }
}
