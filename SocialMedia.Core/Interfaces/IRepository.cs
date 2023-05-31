using SocialMedia.Core.Entities;

namespace SocialMedia.Core.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> GetFiltered(Func<TEntity, bool> predicate);
    Task<TEntity> FindById(int id);
    Task<TEntity> FindByIdWithCollections(int id, params string[] includeProperties);
    Task<TEntity> Add(TEntity entity);
    Task Delete(int id);
    void Update(TEntity entity);
    Task Save();
}
