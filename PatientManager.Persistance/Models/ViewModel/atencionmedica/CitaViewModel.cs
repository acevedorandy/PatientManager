

using System.ComponentModel.DataAnnotations;

namespace PatientManager.Persistance.Models.ViewModel.atencionmedica
{
    public class CitaViewModel
    {
        public int CitaID { get; set; }

        [Display(Name ="Nomre del paciente")]
        public string NombrePaciente { get; set; }

        [Display(Name = "Nomre del medico")]
        public string NombreMedico { get; set; }

        [Display(Name = "Fecha de la cita")]
        public DateTime FechaCita { get; set; }

        public TimeSpan Hora { get; set; }
        public string Causa { get; set; }
        public string Estado { get; set; }
        public int ConsultorioID { get; set; }
    }
}
