using Microsoft.AspNetCore.Mvc;
using PatientManager.Application.Contracts.laboratorio;
using PatientManager.Application.Dtos.laboratorio;
using PatientManager.Persistance.Models.laboratorio;
using PatientManager.Web.HelpersWeb.administracion;

namespace PatientManager.Web.Controllers.laboratorio
{
    public class PruebasLaboratorioController : Controller
    {
        private readonly IPruebasLaboratorioService _pruebasLaboratorioService;
        private readonly UsuarioHelper _usuarioHelper;

        public PruebasLaboratorioController(IPruebasLaboratorioService pruebasLaboratorioService, UsuarioHelper usuarioHelper)
        {
            _pruebasLaboratorioService = pruebasLaboratorioService;
            _usuarioHelper = usuarioHelper;
        }

        public async Task <IActionResult> Index()
        {
            var result = await _pruebasLaboratorioService.GetAll();

            if (result.IsSuccess)
            {
                List<PruebasLaboratorioModel> pruebasLaboratorioModels = (List<PruebasLaboratorioModel>)result.Model;
                return View(pruebasLaboratorioModels);
            }
            return View();
        }

        public async Task <IActionResult> Details(int id)
        {
            var result = await _pruebasLaboratorioService.GetByID(id);

            if (result.IsSuccess)
            {
                PruebasLaboratorioModel pruebasLaboratorioModel = (PruebasLaboratorioModel)result.Model;
                return View(pruebasLaboratorioModel);
            }
            return View();
        }

        public async Task <ActionResult> Create()
        {
            var userConsultorio = await _usuarioHelper.GetConsultorioAdmin();

            ViewBag.Consultorio = userConsultorio;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(PruebasLaboratorioDto pruebasLaboratorioDto)
        {
            try
            {
                var result = await _pruebasLaboratorioService.SaveAsync(pruebasLaboratorioDto);

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
            var result = await _pruebasLaboratorioService.GetByID(id);

            if (result.IsSuccess)
            {
                PruebasLaboratorioModel pruebasLaboratorioModel = (PruebasLaboratorioModel)result.Model;
                return View(pruebasLaboratorioModel);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Edit(PruebasLaboratorioDto pruebasLaboratorioDto)
        {
            try
            {
                var result = await _pruebasLaboratorioService.UpdateAsync(pruebasLaboratorioDto);
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
            var result = await _pruebasLaboratorioService.GetByID(id);

            if (result.IsSuccess)
            {
                PruebasLaboratorioModel pruebasLaboratorioModel = (PruebasLaboratorioModel)result.Model;
                return View(pruebasLaboratorioModel);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Delete(PruebasLaboratorioDto pruebasLaboratorioDto)
        {
            try
            {
                var result = await _pruebasLaboratorioService.RemoveAsync(pruebasLaboratorioDto);
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
