using Microsoft.AspNetCore.Mvc;
using PatientManager.Application.Contracts.atencionmedica;
using PatientManager.Application.Dtos.atencionmedica;
using PatientManager.Persistance.Models.atencionmedica;
using PatientManager.Persistance.Models.ViewModel.atencionmedica;
using PatientManager.Web.HelpersWeb.administracion;

namespace PatientManager.Web.Controllers.atencionmedica
{
    public class CitasController : Controller
    {
        private readonly ICitasService _citasService;
        private readonly UsuarioHelper _usuarioHelper;
        private readonly EstadoHelper _estadoHelper;

        public CitasController(ICitasService citasService, UsuarioHelper usuarioHelper, EstadoHelper estadoHelper)
        {
            _citasService = citasService;
            _usuarioHelper = usuarioHelper;
            _estadoHelper = estadoHelper;
        }
        public async Task <IActionResult> Index()
        {
            var result = await _citasService.GetAll();

            if (result.IsSuccess)
            {
                List<CitaViewModel> citasModels = (List<CitaViewModel>)result.Model;
                return View(citasModels);
            }
            return View();
        }

        public async Task <IActionResult> Details(int id)
        {
            var result = await _citasService.GetByID(id);

            if (result.IsSuccess)
            {
                CitasModel citasModel = (CitasModel)result.Model;
                return View(citasModel);
            }
            return View();
        }

        public async Task <ActionResult> Create()
        {
            var patient = await _usuarioHelper.GetPaciente();
            var doctor = await _usuarioHelper.GetMedicos();
            var consultorio = await _usuarioHelper.GetConsultorioAdmin();

            ViewBag.Paciente = patient;
            ViewBag.Doctor = doctor;
            ViewBag.Consultorio = consultorio;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(CitasDto citasDto)
        {
            try
            {
                citasDto.EstadoID = await _estadoHelper.SetEstadoPendiente();

                if (citasDto.EstadoID == 0)
                {
                    ViewBag.Message = "No se pudo establecer el estado.";
                    return View(citasDto);
                }
                var result = await _citasService.SaveAsync(citasDto);

                if (result.IsSuccess) 
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Paciente = await _usuarioHelper.GetPaciente();
                    ViewBag.Doctor = await _usuarioHelper.GetMedicos();
                    ViewBag.Consultorio = await _usuarioHelper.GetConsultorioAdmin();
                    ViewBag.Estado = await _estadoHelper.GetAllEstados();
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        public async Task <IActionResult> Edit(int id)
        {

            ViewBag.Paciente = await _usuarioHelper.GetPaciente();
            ViewBag.Doctor = await _usuarioHelper.GetMedicos();
            ViewBag.Consultorio = await _usuarioHelper.GetConsultorioAdmin();
            ViewBag.Estado = await _estadoHelper.GetAllEstados();

            var result = await _citasService.GetByID(id);

            if (result.IsSuccess)
            {
                CitasModel citasModel = (CitasModel)result.Model;
                return View(citasModel);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Edit(CitasDto citasDto)
        {
            try
            {
                var result = await _citasService.UpdateAsync(citasDto);

                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = result.Messages;
                    return View();
                }
            }
            catch
            {
                ViewBag.Estado = await _estadoHelper.GetAllEstados();
                return View();
            }
        }

        public async Task <IActionResult> Delete(int id)
        {
            var result = await _citasService.GetByID(id);

            if (result.IsSuccess)
            {
                CitasModel citasModel = (CitasModel)result.Model;
                return View(citasModel);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Delete(CitasDto citasDto)
        {
            try
            {
                var result = await _citasService.RemoveAsync(citasDto);

                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = result.Messages;
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
