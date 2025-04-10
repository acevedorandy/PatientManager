using Microsoft.AspNetCore.Mvc;
using PatientManager.Application.Contracts.atencionmedica;
using PatientManager.Application.Dtos.atencionmedica;
using PatientManager.Persistance.Models.atencionmedica;
using PatientManager.Web.HelpersWeb.administracion;
using PatientManager.Web.HelpersWeb.atencionmedica;


namespace PatientManager.Web.Controllers.atencionmedica
{
    public class MedicosController : Controller
    {
        private readonly IMedicosService _medicosService;
        private readonly UsuarioHelper _usuarioHelper;
        private readonly MedicoHelper _medicoHelper;

        public MedicosController(IMedicosService medicosService, UsuarioHelper usuarioHelper,
                                MedicoHelper medicoHelper)
        {
            _medicosService = medicosService;
            _usuarioHelper = usuarioHelper;
            _medicoHelper = medicoHelper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _medicosService.GetAll();
            if (result.IsSuccess)
            {
                List<MedicosModel> medicosModels = (List<MedicosModel>)result.Model;
                return View(medicosModels);
            }
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _medicosService.GetByID(id);
            if (result.IsSuccess)
            {
                MedicosModel medicosModel = (MedicosModel)result.Model;
                return View(medicosModel);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Consultorio = await _usuarioHelper.GetConsultorioAdmin();
            return View(new MedicosDto()); // Asegura que se pase un modelo vacío
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MedicosDto medicosDto)
        {
            try
            {
                var result = await _medicosService.SaveAsync(medicosDto);

                if (result.IsSuccess && medicosDto.MedicoID != 0)
                {
                    // Cargar archivo solo si se ha subido una imagen
                    if (medicosDto.File != null)
                    {
                        medicosDto.Foto = _medicoHelper.UpLoadImageTo(medicosDto.File, medicosDto.MedicoID);
                        await _medicosService.UpdateAsync(medicosDto);
                    }

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = result.Messages;
                    ViewBag.Consultorio = await _usuarioHelper.GetConsultorioAdmin();
                    return View(medicosDto);
                }
            }
            catch
            {
                ViewBag.Message = "Error al crear el médico.";
                ViewBag.Consultorio = await _usuarioHelper.GetConsultorioAdmin();
                return View(medicosDto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Consultorio = await _usuarioHelper.GetConsultorioAdmin();
            var result = await _medicosService.GetDoctorConvertion(id);

            if (result.IsSuccess && result.Model is MedicosDto medicosDto)
            {
                return View("Create", medicosDto); // Reutiliza la vista de Create
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MedicosDto medicosDto)
        {
            try
            {
                var dto = await _medicosService.GetByID(medicosDto.MedicoID);

                if (medicosDto.File != null) // Solo actualizar la foto si se sube una nueva
                {
                    medicosDto.Foto = _medicoHelper.UpLoadImageTo(medicosDto.File, medicosDto.MedicoID, true, medicosDto.Foto);
                }
                else
                {
                    medicosDto.Foto = ((MedicosDto)dto.Model).Foto; // Mantener la imagen anterior
                }

                var result = await _medicosService.UpdateAsync(medicosDto);

                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = result.Messages;
                    ViewBag.Consultorio = await _usuarioHelper.GetConsultorioAdmin();
                    return View("Create", medicosDto);
                }
            }
            catch
            {
                ViewBag.Message = "Error al actualizar el médico.";
                ViewBag.Consultorio = await _usuarioHelper.GetConsultorioAdmin();
                return View("Create", medicosDto);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _medicosService.GetByID(id);
            if (result.IsSuccess)
            {
                MedicosModel medicosModel = (MedicosModel)result.Model;
                return View(medicosModel);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(MedicosDto medicosDto)
        {
            try
            {
                var result = await _medicosService.RemoveAsync(medicosDto);
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
                ViewBag.Message = "Error al eliminar el médico.";
                return View();
            }
        }
    }
}
