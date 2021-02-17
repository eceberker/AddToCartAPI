using AddToCartAPI.Model.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AddToCartAPI.Model.Repository
{
    public class EntityFrameWorkRepository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        public EntityFrameWorkRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        protected DbContext Context { get; }

        public void Create<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public Task SaveAsync()
        {
            try
            {
                return _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                ThrowEnhancedValidationException(e);
            }

            return Task.FromResult(0);
        }


        public TEntity GetById<TEntity>(object id) where TEntity : class
        {
            return _dbContext.Set<TEntity>().Find(id);
        }


        protected virtual void ThrowEnhancedValidationException(DbUpdateException e)
        {
            throw new DbUpdateException(e.Message, e.InnerException);
        }

        public IQueryable<T> Query()
        {
            var DbSet = _dbContext.Set<T>();
            return DbSet;
        }

        public void Add(T entity)
        {
            var DbSet = _dbContext.Set<T>();

            DbSet.Add(entity);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Attach(T entity)
        {
            var updatedEntitiy = _dbContext.Set<T>().Attach(entity);
            updatedEntitiy.State = EntityState.Modified;

        }
    }
}
