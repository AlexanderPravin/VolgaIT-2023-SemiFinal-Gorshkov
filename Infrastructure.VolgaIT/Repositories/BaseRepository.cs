using Domain.VolgaIT.Interfaces;
using Infrastructure.VolgaIT.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Infrastructure.VolgaIT.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class 
    {
        internal readonly VolgaContext _context;

        public BaseRepository(VolgaContext context) 
        { 
            _context = context;
        }

        public virtual void AddEntity(TEntity entity)
        {
            _context.Set<TEntity>().AddAsync(entity);
        }

        public virtual void DeleteEntity(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public virtual async Task<ICollection<TEntity>> GetAllEntitiesAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<ICollection<TEntity>> GetEntitiesByAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _context.Set<TEntity>().Where(filter).ToListAsync();
        }

        public virtual async Task<TEntity?> GetEntityByAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(filter);
        }

        public virtual async Task<TEntity?> GetEntityByIdAsync(string id)
        {
            return await _context.Set<TEntity>().FindAsync(Guid.Parse(id));
        }

        public virtual void UpdateEntity(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public virtual async Task<ICollection<TEntity>> GetEntitiesInRangeAsync(int start, int count)
        {
            if (start + count > _context.Set<TEntity>().Count()) throw new ArgumentException("Index was out of range");

            return await _context.Set<TEntity>().Skip(start).Take(count).ToListAsync();
        }
    }
}
