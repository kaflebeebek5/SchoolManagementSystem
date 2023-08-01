using Shared.Wrapper;

namespace AuthApi.Repositories
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        Task<int> CompleteAsync();
        void Add<TEntity>(TEntity entity) where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class;
        void Delete<TEntity>(TEntity entity) where TEntity : class;
        Task<TEntity> GetByIdAsync<TEntity>(int id) where TEntity : class;
        Task<List<TEntity>> GetAllAsync<TEntity>() where TEntity : class;
    }
}
