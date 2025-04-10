

using System.ComponentModel.DataAnnotations;

namespace PatientManager.Application.Dtos.administracion
{
    public class ConsultoriosDto
    {
        public int ConsultorioID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El nombre del consultorio no puede superar los 100 caracteres.")]
        public string NombreConsultorio { get; set; }

    }
}
