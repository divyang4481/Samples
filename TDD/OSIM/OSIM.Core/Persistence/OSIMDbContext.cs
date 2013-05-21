using System.Data.Entity;
using OSIM.Core.Entities;

namespace OSIM.Core.Persistence
{
    public class OsimDbContext : DbContext, IDbContext
    {
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
