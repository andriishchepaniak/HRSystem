using HR.DAL.Interfaces;
using HR.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HR.DAL.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IBaseEntity
    {
        protected readonly AppDbContext db;
        public BaseRepository(AppDbContext context)
        {
            db = context;
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            await db.Set<TEntity>().AddAsync(entity);
            await db.SaveChangesAsync();
            return entity;
            
        }

        public async Task<int> DeleteAsync(int id)
        {
            var entity = await db.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
            db.Set<TEntity>().Remove(entity);
            return await db.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await db.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllWithPage(int offset, int count)
        {
            return await db.Set<TEntity>()
                            .Skip(offset)
                            .Take(count)
                            .ToListAsync();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await db.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public  async Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return await db.Set<TEntity>()
                            .Where(predicate)
                            .ToListAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            db.Set<TEntity>().Update(entity);
            await db.SaveChangesAsync();
            return entity;
        }
    }
}
