using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PatientManager.Application.Dtos.atencionmedica
{
    public class PacientesDto
    {
        public int PacienteID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El apellido no puede superar los 100 caracteres.")]
        public string Apellido { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "El telefono no puede superar los 20 caracteres.")]
        public string Telefono { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "La direccion no puede superar los 255 caracteres.")]
        public string Direccion { get; set; }

        [Required]
        [StringLength(11, ErrorMessage = "La cedula no puede superar los 11 caracteres.")]
        public string Cedula { get; set; }

        public DateTime FechaNacimiento { get; set; }
        public bool Fumador { get; set; }
        public string? Alergias { get; set; }
        public string? Foto { get; set; }

        [Required]
        public int ConsultorioID { get; set; }

        public IFormFile? File { get; set; } // AHORA ES OPCIONAL
    }
}
