using Microsoft.AspNetCore.Mvc;
using PatientManager.Application.Contracts.atencionmedica;
using PatientManager.Application.Dtos.atencionmedica;
using PatientManager.Persistance.Models.atencionmedica;
using PatientManager.Web.HelpersWeb.administracion;
using PatientManager.Web.HelpersWeb.atencionmedica;

namespace PatientManager.Web.Controllers.atencionmedica
{
    public class PacientesController : Controller
    {
        private readonly IPacientesService _pacientesService;
        private readonly ValidateUserSesion _validateUserSesion;
        private readonly UsuarioHelper _usuarioHelper;
        private readonly PacienteHelper _pacienteHelper;

        public PacientesController(IPacientesService pacientesService, ValidateUserSesion validateUserSesion,
                                    UsuarioHelper usuarioHelper, PacienteHelper pacienteHelper)
        {
            _pacientesService = pacientesService;
            _validateUserSesion = validateUserSesion;
            _usuarioHelper = usuarioHelper;
            _pacienteHelper = pacienteHelper;
        }
        public async Task <IActionResult> Index()
        {
            var result = await _pacientesService.GetAll();

            if (result.IsSuccess)
            {
                List<PacientesModel> pacientesModels = (List<PacientesModel>)result.Model;
                return View(pacientesModels);
            }
            return View();
        }

        public async Task <IActionResult> Details(int id)
        {
            var result = await _pacientesService.GetByID(id);

            if (result.IsSuccess)
            {
                PacientesModel pacientesModel = (PacientesModel)result.Model;
                return View(pacientesModel);
            }
            return View();
        }

        public async Task <ActionResult> Create()
        {
            var userConsultorio = await _usuarioHelper.GetConsultorioAdmin();
            ViewBag.Consultorio = userConsultorio;

            return View(new PacientesDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(PacientesDto pacientesDto)
        {
            try
            {
                var result = await _pacientesService.SaveAsync(pacientesDto);

                if (result.IsSuccess && pacientesDto.PacienteID != 0)
                {
                    if (pacientesDto.File != null)
                    {
                        pacientesDto.Foto = _pacienteHelper.UpLoadImageTo(pacientesDto.File, pacientesDto.PacienteID);
                        await _pacientesService.UpdateAsync(pacientesDto);
                    }

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = result.Messages;
                    ViewBag.Consultorio = await _usuarioHelper.GetConsultorioAdmin();
                    return View(pacientesDto);
                }
            }
            catch
            {
                ViewBag.Message = "Error al crear el médico.";
                ViewBag.Paciente = await _usuarioHelper.GetConsultorioAdmin();
                return View(pacientesDto);
            }
        }

        public async Task <IActionResult> Edit(int id)
        {
            ViewBag.Paciente = await _usuarioHelper.GetConsultorioAdmin();
            var result = await _pacientesService.GetPacienteConvertion(id);

            if (result.IsSuccess && result.Model is PacientesDto pacienteDto)
            {
                return View("Create", pacienteDto);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Edit(PacientesDto pacientesDto)
        {
            try
            {
                var dto = await _pacientesService.GetByID(pacientesDto.PacienteID);

                if (pacientesDto.File != null) 
                {
                    pacientesDto.Foto = _pacienteHelper.UpLoadImageTo(pacientesDto.File, pacientesDto.PacienteID, true, pacientesDto.Foto);
                }
                else
                {
                    pacientesDto.Foto = ((PacientesDto)dto.Model).Foto;
                }

                var result = await _pacientesService.UpdateAsync(pacientesDto);

                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = result.Messages;
                    ViewBag.Paciente = await _usuarioHelper.GetConsultorioAdmin();
                    return View("Create", pacientesDto);
                }
            }
            catch
            {
                ViewBag.Message = "Error al actualizar el médico.";
                ViewBag.Paciente = await _usuarioHelper.GetConsultorioAdmin();
                return View("Create", pacientesDto);
            }
        }

        public async Task <IActionResult> Delete(int id)
        {
            var result = await _pacientesService.GetByID(id);

            if (result.IsSuccess)
            {
                PacientesModel pacientesModel = (PacientesModel)result.Model;
                return View(pacientesModel);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Delete(PacientesDto pacientesDto)
        {
            try
            {
                var result = await _pacientesService.RemoveAsync(pacientesDto);

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
