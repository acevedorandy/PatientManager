using PatientManager.Persistance.Models.administracion;
using Microsoft.AspNetCore.Mvc;
using PatientManager.Application.Contracts.administracion;
using PatientManager.Application.Dtos.administracion;

namespace PatientManager.Web.Controllers.administracion
{
    public class ConsultoriosController : Controller
    {
        private readonly IConsultoriosServices _consultoriosServices;

        public ConsultoriosController(IConsultoriosServices consultoriosServices)
        {
            _consultoriosServices = consultoriosServices;
        }

        public async Task <IActionResult> Index()
        {
            var result = await _consultoriosServices.GetAll();
            
            if (result.IsSuccess)
            {
                List<ConsultoriosModel> consultoriosModels = (List<ConsultoriosModel>)result.Model;
                return View(consultoriosModels);
            }
            return View();
        }


        public async Task <IActionResult> Details(int id)
        {
            var result = await _consultoriosServices.GetByID(id);

            if (result.IsSuccess)
            {
                ConsultoriosModel consultoriosModel = (ConsultoriosModel)result.Model; 
                return View(consultoriosModel);
            }
            return View();
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(ConsultoriosDto consultoriosDto)
        {
            try
            {
                var resul = await _consultoriosServices.SaveAsync(consultoriosDto);

                if (resul.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = resul.Messages;
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
            var result = await _consultoriosServices.GetByID(id);

            if (result.IsSuccess)
            {
                ConsultoriosModel consultoriosModel = (ConsultoriosModel)result.Model;
                return View(consultoriosModel);
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Edit(ConsultoriosDto consultoriosDto)
        {
            try
            {
                var result = await _consultoriosServices.UpdateAsync(consultoriosDto);

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
            var result = await _consultoriosServices.GetByID(id);

            if (result.IsSuccess)
            {
                ConsultoriosModel consultoriosModel = (ConsultoriosModel)result.Model;
                return View(consultoriosModel);
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Delete(ConsultoriosDto consultoriosDto)
        {
            try
            {
               var result = await _consultoriosServices.RemoveAsync(consultoriosDto);

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
