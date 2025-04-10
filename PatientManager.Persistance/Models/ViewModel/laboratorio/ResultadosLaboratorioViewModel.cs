

using System.ComponentModel.DataAnnotations;

namespace PatientManager.Persistance.Models.ViewModel.laboratorio
{
    public class ResultadosLaboratorioViewModel
    {
        public int ResultadoID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }

        [Display(Name ="Nombre de la prueba")]
        public string NombrePrueba { get; set; }
        public string Resultado { get; set; }
        public int ConsultorioID { get; set; }

        public int EstadoID { get; set; }
    }
}
