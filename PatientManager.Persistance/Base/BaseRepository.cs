using Microsoft.EntityFrameworkCore;
using PatientManager.Domain.Repositories;
using PatientManager.Domain.Result;
using PatientManager.Persistance.Context;
using System.Linq.Expressions;

namespace PatientManager.Persistance.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly PatientManagerContext patientManager_Context;
        private DbSet<TEntity> entities;

        public BaseRepository(PatientManagerContext patientManagerContext)
        {
            patientManager_Context = patientManagerContext;
            this.entities = patientManagerContext.Set<TEntity>();
        }

        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> filter)
        {
            return await this.entities.AnyAsync(filter);
        }
        public virtual async Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();

            try
            {
                var datos = await this.entities.ToListAsync();
                result.Data = datos;
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "Hubo un error obteniendo las entidades.";
            }
            return result;
        }
        public virtual async Task<OperationResult> GetById(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                var entity = await this.entities.FindAsync(id);
                result.Data = entity;
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "hubo un error obteniendo la entidad.";
            }
            return result;
        }
        public virtual async Task<OperationResult> Remove(TEntity entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                entities.Remove(entity);
                await patientManager_Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "Hubo un error eliminando la entidad.";
            }
            return result;
        }
        public virtual async Task<OperationResult> Save(TEntity entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                entities.Add(entity);
                await patientManager_Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "Hubo un error guardando la entidad.";
            }
            return result;
        }
        public virtual async Task<OperationResult> Update(TEntity entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                entities.Update(entity);
                await patientManager_Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "Hubo un error actualizando la entidad.";
            }
            return result;
        }
    }
}

