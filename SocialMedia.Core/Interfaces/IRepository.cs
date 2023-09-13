using SocialMedia.Core.Entities;

namespace SocialMedia.Core.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> GetFiltered(Func<TEntity, bool> predicate);
    IQueryable<TEntity> AsQueryable();
    Task<TEntity> FindByIdAsync(int id);
    Task<TEntity> FindByIdWithCollections(int id, params string[] includeProperties);
    Task<TEntity> AddAsync(TEntity entity);
    Task Delete(int id);
    void Update(TEntity entity);
}
