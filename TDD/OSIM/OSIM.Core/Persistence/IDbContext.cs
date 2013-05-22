using System.Data.Entity;
using OSIM.Core.Entities;

namespace OSIM.Core.Persistence
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        void Dispose();
    }
}
