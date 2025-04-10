using Microsoft.AspNetCore.Mvc.Rendering;
using PatientManager.Application.Contracts.administracion;
using PatientManager.Persistance.Models.administracion;
using PatientManager.Persistance.Models.atencionmedica;

namespace PatientManager.Web.HelpersWeb.administracion
{
    public class EstadoHelper
    {
        private readonly IEstadosService _estadosService;

        public EstadoHelper(IEstadosService estadosService)
        {
            _estadosService = estadosService;
        }

        public async Task<int> SetEstadoPendiente()
        {
            try
            {
                var response = await _estadosService.GetAll();

                if (response.IsSuccess && response.Model is List<EstadosModel> estado)
                {
                    var estadoPendiente = estado.FirstOrDefault(e => e.EstadoID == 1); // Obtener el estado con ID 1

                    if (estadoPendiente != null)
                    {
                        return estadoPendiente.EstadoID; // Retorna el ID directamente como int
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el estado pendiente: {ex.Message}");
            }
            return 0; // Retorna 0 si no se encuentra el estado
        }

        public async Task<int> SetEstadoCompletado()
        {
            try
            {
                var response = await _estadosService.GetAll();

                if (response.IsSuccess && response.Model is List<EstadosModel> estado)
                {
                    var estadoPendiente = estado.FirstOrDefault(e => e.EstadoID == 3); // Obtener el estado con ID 1

                    if (estadoPendiente != null)
                    {
                        return estadoPendiente.EstadoID; // Retorna el ID directamente como int
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el estado pendiente: {ex.Message}");
            }
            return 0; // Retorna 0 si no se encuentra el estado
        }

        public async Task<List<SelectListItem>> GetAllEstados()
        {
            var estadosList = new List<SelectListItem>();

            try
            {
                var response = await _estadosService.GetAll();

                if (response.IsSuccess && response.Model is List<EstadosModel> estados)
                {
                    estadosList = estados.Select(p => new SelectListItem
                    {
                        Text = p.Nombre,
                        Value = p.EstadoID.ToString()
                    }).ToList();
                }
            }
            catch (Exception)
            {
            }
            return estadosList;
        }
    }
}
