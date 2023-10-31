using System.Linq.Expressions;

namespace Domain.VolgaIT.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void AddEntity(T entity);
        void DeleteEntity(T entity);
        void UpdateEntity(T entity);
        Task<ICollection<T>> GetAllEntitiesAsync();
        Task<ICollection<T>> GetEntitiesInRangeAsync(int min, int max);
        Task<ICollection<T>> GetEntitiesByAsync(Expression<Func<T, bool>> filter);
        Task<T?> GetEntityByIdAsync(string id);
        Task<T?> GetEntityByAsync(Expression<Func<T, bool>> filter);
    }
}
