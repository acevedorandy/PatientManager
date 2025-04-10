using Microsoft.AspNetCore.Mvc;
using PatientManager.Application.Contracts.administracion;
using PatientManager.Persistance.Models.ViewModel.administracion.usuario;
using PatientManager.Web.HelpersWeb.administracion.usuario;
using PatientManager.Application.Helpers.web;
using PatientManager.Web.HelpersWeb.administracion;
using PatientManager.Web.Controllers.HelperController;

namespace PatientManager.Web.Controllers.login
{
    public class LoginController : Controller 
    {
        private readonly IUsuariosService _usuariosService;
        private readonly RegisterHelper _registerHelper;
        private readonly ValidateUserSesion _validateUserSesion;
        public LoginController(IUsuariosService usuariosService, RegisterHelper registerHelper, ValidateUserSesion validateUserSesion)
        {
            _usuariosService = usuariosService;
            _registerHelper = registerHelper;
            _validateUserSesion = validateUserSesion;
        }

        [IgnoreSessionValidation]
        public async Task<IActionResult> Index()
        {
            if (_validateUserSesion.HasUser())
            {
                return RedirectToRoute(new { Controller = "Login", action = "Index" });
            }
            ModelState.Clear();
            return View();
        }


        [HttpPost]
        public async Task <IActionResult> Index(LogInUserViewModel logInUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(logInUserViewModel);
            }

            UsuariosViewModel usuariosView = await _usuariosService.Login(logInUserViewModel);

            if (usuariosView != null)
            {
                HttpContext.Session.Set<UsuariosViewModel>("usuario", usuariosView);
                //HttpContext.Session.Set<UsuariosViewModel>("usuario", usuariosView);
                return RedirectToRoute(new { Controller = "Home", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("UserValidation", "Datos de acceso incorrectos.");
            }
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("usuario");
            return RedirectToRoute(new { Controller = "Login", action = "Index" });

        }

        [IgnoreSessionValidation]
        public async Task <IActionResult> Register()
        {
            var tiposUsuarios = await  _registerHelper.GetUserType();

            ViewBag.TiposUsuarios = tiposUsuarios;

            return View(new SaveUserViewModel());
        }

        [IgnoreSessionValidation]
        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel saveUserViewModel)
        {
            if (_validateUserSesion.HasUser())
            {
                return RedirectToRoute(new { Controller = "Login", action = "Register" });
            }

            if (!ModelState.IsValid)
            {
                return View(saveUserViewModel);
            }

            var result = await _usuariosService.SaveAsync(saveUserViewModel);

            if (result.IsSuccess)
            {
                return RedirectToRoute(new { Controller = "Login", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("NombreUsuario", result.Messages);
                return View(saveUserViewModel);
            }
        }
    }
}
