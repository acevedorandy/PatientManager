

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PatientManager.Domain.Entities.atencionmedica
{
    [Table("Pacientes", Schema = "atencionmedica")]

    public class Pacientes
    {
        [Key]
        public int PacienteID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Cedula { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Fumador { get; set; }
        public string? Alergias { get; set; }
        public string? Foto { get; set; }
        public int ConsultorioID { get; set; }

    }
}
