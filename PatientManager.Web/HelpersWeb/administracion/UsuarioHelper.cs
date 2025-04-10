using Microsoft.AspNetCore.Mvc.Rendering;
using PatientManager.Application.Contracts.administracion;
using PatientManager.Persistance.Models.administracion;
using PatientManager.Persistance.Models.ViewModel.administracion.usuario;
using PatientManager.Application.Helpers.web;
using PatientManager.Persistance.Models.atencionmedica;
using PatientManager.Application.Contracts.atencionmedica;
using PatientManager.Application.Contracts.laboratorio;
using PatientManager.Persistance.Models.laboratorio;

namespace PatientManager.Web.HelpersWeb.administracion
{
    public class UsuarioHelper
    {
        private readonly IConsultoriosServices _consultoriosServices;
        private readonly IUsuariosService _usuariosService;
        private readonly IMedicosService _medicosService;
        private readonly IPacientesService _pacientesService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPruebasLaboratorioService _pruebasLaboratorioService;
        private readonly UsuariosViewModel _usuarioViewModel;

        public UsuarioHelper(IConsultoriosServices consultoriosServices, IUsuariosService usuariosService, IHttpContextAccessor contextAccessor
            ,IPacientesService pacientesService, IMedicosService medicosService, IPruebasLaboratorioService pruebasLaboratorioService)
        {
            _consultoriosServices = consultoriosServices;
            _usuariosService = usuariosService;
            _httpContextAccessor = contextAccessor;
            _pacientesService = pacientesService;
            _medicosService = medicosService;
            _pruebasLaboratorioService = pruebasLaboratorioService;
            _usuarioViewModel = _httpContextAccessor.HttpContext.Session.Get<UsuariosViewModel>("usuario");
        }

        public async Task<List<SelectListItem>> GetConsultorioAdmin()
        {
            var consultorioList = new List<SelectListItem>();

            try
            {
                // Obtener el usuario desde la sesión
                var usuarioViewModel = _httpContextAccessor.HttpContext.Session.Get<UsuariosViewModel>("usuario");

                // Obtener el usuario por su ID
                var usuario = (await _usuariosService.GetByID(usuarioViewModel.UsuarioID)).Model as UsuariosModel;

                // Obtener el consultorio asociado al usuario
                var consultorio = (await _consultoriosServices.GetByID(usuario.ConsultorioID)).Model as ConsultoriosModel;

                // Agregar el consultorio a la lista de SelectListItem
                if (consultorio != null)
                {
                    consultorioList.Add(new SelectListItem
                    {
                        Text = consultorio.NombreConsultorio,
                        Value = consultorio.ConsultorioID.ToString()
                    });
                }
            }
            catch (Exception)
            {
            }
            return consultorioList;
        }

        public async Task<List<SelectListItem>> GetPaciente()
        {
            var pacienteList = new List<SelectListItem>();

            try
            {
                var response = await _pacientesService.GetAll();

                if (response.IsSuccess && response.Model is List<PacientesModel> pacientes)
                {
                    pacienteList = pacientes.Select(p => new SelectListItem
                    {
                        Text = p.Nombre,
                        Value = p.PacienteID.ToString()
                    }).ToList();
                }
            }
            catch (Exception)
            {
            }
            return pacienteList;
        }

        public async Task<List<SelectListItem>> GetMedicos()
        {
            var medicoList = new List<SelectListItem>();

            try
            {
                var response = await _medicosService.GetAll();

                if (response.IsSuccess && response.Model is List<MedicosModel> medico)
                {
                    medicoList = medico.Select(m => new SelectListItem
                    {
                        Text = m.Nombre,
                        Value = m.MedicoID.ToString()
                    }).ToList();
                }
            }
            catch (Exception)
            {
            }
            return medicoList;
        }

        public async Task<List<SelectListItem>> GetPruebas()
        {
            var pruebasList = new List<SelectListItem>();

            try
            {
                var response = await _pruebasLaboratorioService.GetAll();

                if (response.IsSuccess && response.Model is List<PruebasLaboratorioModel> pruebasLaboratorio)
                {
                    pruebasList = pruebasLaboratorio.Select(p => new SelectListItem
                    {
                        Text = p.NombrePrueba,
                        Value = p.PruebaID.ToString()
                    }).ToList();
                }
            }
            catch (Exception)
            {
            }
            return pruebasList;
        }

        public async Task<List<SelectListItem>> SetRol()
        {
            return await Task.FromResult(new List<SelectListItem>
            {
                new SelectListItem { Text = "Administrador", Value = "Administrador" },
                new SelectListItem { Text = "Asistente", Value = "Asistente" }
            });
        }
    }
}
