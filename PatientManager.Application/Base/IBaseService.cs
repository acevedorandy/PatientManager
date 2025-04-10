using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManager.Application.Base
{
    public interface IBaseService<TResponse, TEntityDto>
    {
        Task<TResponse> SaveAsync (TEntityDto dto);
        Task<TResponse> UpdateAsync (TEntityDto dto);
        Task<TResponse> RemoveAsync (TEntityDto dto);
        Task<TResponse> GetAll();
        Task<TResponse> GetByID (int id);
    }
}
