using PatientManager.Application.Base;
using PatientManager.Application.Core;
using PatientManager.Application.Dtos.administracion;
using PatientManager.Persistance.Models.ViewModel.administracion.usuario;


namespace PatientManager.Application.Contracts.administracion
{
    public interface IUsuariosService : IBaseService<ServiceResponse, SaveUserViewModel>
    {
        Task<UsuariosViewModel> Login(LogInUserViewModel logInUserViewModel);
        Task<ServiceResponse> SaveAsistenteOnly(UsuariosDto usuariosDto);
    }
}
