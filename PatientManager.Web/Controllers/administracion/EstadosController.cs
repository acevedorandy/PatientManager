using Microsoft.AspNetCore.Mvc;
using PatientManager.Application.Contracts.administracion;
using PatientManager.Application.Dtos.administracion;
using PatientManager.Persistance.Models.administracion;

namespace PatientManager.Web.Controllers.administracion
{
    public class EstadosController : Controller
    {
        private readonly IEstadosService _estadosService;

        public EstadosController(IEstadosService estadosService)
        {
            _estadosService = estadosService;
        }

        public async Task <IActionResult> Index()
        {
            var result = await _estadosService.GetAll();

            if (result.IsSuccess)
            {
                List<EstadosModel> estadosModels = (List<EstadosModel>)result.Model;

                return View(estadosModels);
            }
            return View();
        }

        public async Task <IActionResult> Details(int id)
        {
            var result = await _estadosService.GetByID(id);

            if (result.IsSuccess)
            {
                EstadosModel estadosModel = (EstadosModel)result.Model;
                
                return View(estadosModel);
            }
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(EstadosDto estadosDto)
        {
            try
            {
                var result = await _estadosService.SaveAsync(estadosDto);

                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                else 
                {
                    ViewBag.Messages = result.Messages;
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
            var result = await _estadosService.GetByID(id);

            if (result.IsSuccess)
            {
                EstadosModel estadosModel = (EstadosModel)result.Model;

                return View(estadosModel);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Edit(EstadosDto estadosDto)
        {
            try
            {
                var result = await _estadosService.UpdateAsync(estadosDto);

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
            var result = await _estadosService.GetByID(id);

            if (result.IsSuccess)
            {
                EstadosModel estadosModel = (EstadosModel)result.Model;

                return View(estadosModel);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Delete(EstadosDto estadosDto)
        {
            try
            {
                var result = await _estadosService.RemoveAsync(estadosDto);

                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = result.Messages;
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
