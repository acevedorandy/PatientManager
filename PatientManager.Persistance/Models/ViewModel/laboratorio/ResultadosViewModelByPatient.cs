

namespace PatientManager.Persistance.Models.ViewModel.laboratorio
{
    public class ResultadosViewModelByPatient
    {
        public int ResultadoID { get; set; }
        public int PacienteID { get; set; }
        public int PruebaID { get; set; }
        public string NombrePrueba { get; set; }
        public string EstadoID { get; set; }
        public string Estado { get; set; }
        public string Resultados { get; set; }
    }
}
