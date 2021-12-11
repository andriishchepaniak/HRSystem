using HR.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HR.DAL.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class, IBaseEntity
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetAllWithPage(int offset, int count);
        Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetById(int id);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<int> DeleteAsync(int id);
    }
}
