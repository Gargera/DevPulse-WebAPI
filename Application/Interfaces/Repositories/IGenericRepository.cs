using System.Linq.Expressions;

namespace Application.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity>
    {
        public Task<IEnumerable<TEntity>> GetAllEntitiesAsync(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[] includes);

        public Task<TEntity?> GetEntityByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes);

        public Task AddEntityAsync(TEntity entity);

        public void UpdateEntity(TEntity entity);

        public Task DeleteEntityAsync(int id);

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        public Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null);
    }
}
