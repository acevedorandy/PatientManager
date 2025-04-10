using Microsoft.AspNetCore.Mvc;
using PatientManager.Application.Contracts.laboratorio;
using PatientManager.Application.Dtos.laboratorio;
using PatientManager.Persistance.Models.laboratorio;
using PatientManager.Persistance.Models.ViewModel.laboratorio;
using PatientManager.Web.HelpersWeb.administracion;

namespace PatientManager.Web.Controllers.laboratorio
{
    public class ResultadosLaboratorioController : Controller
    {
        private readonly IResultadosLaboratorioService _resultadosLaboratorioService;
        private readonly UsuarioHelper _usuarioHelper;
        private readonly EstadoHelper _estadoHelper;

        public ResultadosLaboratorioController(IResultadosLaboratorioService resultadosLaboratorioService,
                                                UsuarioHelper usuarioHelper, EstadoHelper estadoHelper)
        {
            _resultadosLaboratorioService = resultadosLaboratorioService;
            _usuarioHelper = usuarioHelper;
            _estadoHelper = estadoHelper;
        }

        public async Task <IActionResult> Index()
        {
            var result = await _resultadosLaboratorioService.GetAll();

            if (result.IsSuccess)
            {
                List<ResultadosLaboratorioViewModel> resultadosLaboratorioModels = (List<ResultadosLaboratorioViewModel>)result.Model;
                return View(resultadosLaboratorioModels);
            }
            return View();
        }

        public async Task<IActionResult> ResultadosByPatient(int id) 
        {
            var result = await _resultadosLaboratorioService.GetResultadosByPatientAsync(id);

            if (result.IsSuccess)
            {
                List<ResultadosViewModelByPatient> byPatients = (List<ResultadosViewModelByPatient>)result.Model;
                return View(byPatients);
            }
            return View();
        }

        public async Task <IActionResult> Details(int id)
        {
            var result = await _resultadosLaboratorioService.GetByID(id);

            if (result.IsSuccess)
            {
                ResultadosLaboratorioModel resultadosLaboratorioModel = (ResultadosLaboratorioModel)result.Model;
                return View(resultadosLaboratorioModel);
            }
            return View();
        }

        public async Task <ActionResult> Create()
        {
            var prueba = await _usuarioHelper.GetPruebas();
            var patient = await _usuarioHelper.GetPaciente();
            var consultorio = await _usuarioHelper.GetConsultorioAdmin();

            ViewBag.Prueba = prueba;
            ViewBag.Paciente = patient;
            ViewBag.Consultorio = consultorio;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(ResultadosLaboratorioDto resultadosLaboratorioDto)
        {
            try
            {
                resultadosLaboratorioDto.EstadoID = await _estadoHelper.SetEstadoPendiente();

                if (resultadosLaboratorioDto.EstadoID == 0)
                {
                    ViewBag.Message = "No se pudo establecer el estado.";
                    return View(resultadosLaboratorioDto);
                }
                var result = await _resultadosLaboratorioService.SaveAsync(resultadosLaboratorioDto);

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

        public async Task <IActionResult> Edit(int id)
        {
            var prueba = await _usuarioHelper.GetPruebas();
            var patient = await _usuarioHelper.GetPaciente();
            var consultorio = await _usuarioHelper.GetConsultorioAdmin();

            ViewBag.Prueba = prueba;
            ViewBag.Paciente = patient;
            ViewBag.Consultorio = consultorio;

            var result = await _resultadosLaboratorioService.GetByID(id);

            if (result.IsSuccess)
            {
                ResultadosLaboratorioModel resultadosLaboratorioModel = (ResultadosLaboratorioModel)result.Model;
                return View(resultadosLaboratorioModel);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Edit(ResultadosLaboratorioDto resultadosLaboratorioDto)
        {
            try
            {
                resultadosLaboratorioDto.EstadoID = await _estadoHelper.SetEstadoCompletado();

                if (resultadosLaboratorioDto.EstadoID == 0)
                {
                    ViewBag.Message = "No se pudo establecer el estado.";
                    return View(resultadosLaboratorioDto);
                }

                var result = await _resultadosLaboratorioService.UpdateAsync(resultadosLaboratorioDto);

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

        public async Task <IActionResult> Delete(int id)
        {
            var result = await _resultadosLaboratorioService.GetByID(id);

            if (result.IsSuccess)
            {
                ResultadosLaboratorioModel resultadosLaboratorioModel = (ResultadosLaboratorioModel)result.Model;
                return View(resultadosLaboratorioModel);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Delete(ResultadosLaboratorioDto resultadosLaboratorioDto)
        {
            try
            {
                var result = await _resultadosLaboratorioService.RemoveAsync(resultadosLaboratorioDto);

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
