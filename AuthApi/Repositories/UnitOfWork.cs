using AuthApi.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Shared.Wrapper;

namespace AuthApi.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly SchoolManagementDbContext _dbContext;
        private IDbContextTransaction? _currentTransaction;

        public UnitOfWork(SchoolManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void BeginTransaction()
        {
            _currentTransaction = _dbContext.Database.BeginTransaction();
        }

        public async Task<int> CompleteAsync()
        {
            try
            {
                var changes = await _dbContext.SaveChangesAsync();
                _currentTransaction?.Commit();
                return changes;
            }
            catch
            {
                _currentTransaction?.Rollback();
                throw;
            }
            finally
            {
                _currentTransaction?.Dispose();
                _currentTransaction = null;
            }
        }
        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<TEntity> GetByIdAsync<TEntity>(int id) where TEntity : class
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<List<TEntity>> GetAllAsync<TEntity>() where TEntity : class
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }
    }
}
