using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        private DbContext Context { get; }

        public void Dispose()
        {
            Context.Dispose();
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new GenericRepository<TEntity>(Context.Set<TEntity>());
        }

        public async Task<int> SaveChangesAsync()
        {           
            return await Context.SaveChangesAsync();
        }
    }
}
