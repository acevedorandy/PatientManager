

using System.ComponentModel.DataAnnotations;

namespace PatientManager.Application.Dtos.administracion
{
    public class EstadosDto
    {
        public int EstadoID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El nombre del estado no puede superar los 50 caracteres.")]
        public string Nombre { get; set; }

    }
}
