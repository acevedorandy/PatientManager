using Microsoft.AspNetCore.Mvc;
using PatientManager.Application.Contracts.administracion;
using PatientManager.Application.Dtos.administracion;
using PatientManager.Persistance.Models.administracion;
using PatientManager.Persistance.Models.ViewModel.administracion.usuario;
using PatientManager.Web.HelpersWeb.administracion;

namespace PatientManager.Web.Controllers.administracion
{
    public class UsuariosController : Controller
    {
        private readonly IUsuariosService _usuariosService;
        private readonly UsuarioHelper _usuarioHelper;

        public UsuariosController(IUsuariosService usuariosService, UsuarioHelper usuarioHelper)
        {
            _usuariosService = usuariosService;
            _usuarioHelper = usuarioHelper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _usuariosService.GetAll();

            if (result.IsSuccess)
            {
                List<UsuariosModel> usuariosModels = (List<UsuariosModel>)result.Model;
                return View(usuariosModels);
            }
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _usuariosService.GetByID(id);

            if (result.IsSuccess)
            {
                UsuariosModel usuariosModel = (UsuariosModel)result.Model;
                return View(usuariosModel);
            }
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var userRol = await _usuarioHelper.SetRol();
            var userConsultorio = await _usuarioHelper.GetConsultorioAdmin();

            ViewBag.Rol = userRol;  
            ViewBag.Consultorio = userConsultorio;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuariosDto usuariosDto)
        {
            try
            {
                var result = await _usuariosService.SaveAsistenteOnly(usuariosDto);

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

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _usuariosService.GetByID(id);
            ViewBag.TipoUsuario = await _usuarioHelper.SetRol();

            if (result.IsSuccess)
            {
                UsuariosModel usuariosModel = (UsuariosModel)result.Model;
                return View(usuariosModel);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SaveUserViewModel usuariosDto)
        {
            try
            {
                var result = await _usuariosService.UpdateAsync(usuariosDto);

                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = result.Messages;
                    ViewBag.TipoUsuario = _usuarioHelper.SetRol();
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _usuariosService.GetByID(id);

            if (result.IsSuccess)
            {
                UsuariosModel usuariosModel = (UsuariosModel)result.Model;
                return View(usuariosModel);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(SaveUserViewModel usuariosDto)
        {
            try
            {
                var result = await _usuariosService.RemoveAsync(usuariosDto);

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
