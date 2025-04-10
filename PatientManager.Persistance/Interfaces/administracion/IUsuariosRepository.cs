

using PatientManager.Domain.Entities.administracion;
using PatientManager.Domain.Repositories;
using PatientManager.Persistance.Models.ViewModel.administracion.usuario;

namespace PatientManager.Persistance.Interfaces.administracion
{
    public interface IUsuariosRepository : IBaseRepository<Usuarios>
    {
        Task<Usuarios> Login(LogInUserViewModel logInUserViewModel);

    }
}
