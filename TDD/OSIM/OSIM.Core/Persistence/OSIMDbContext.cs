using System.Data.Entity;
using OSIM.Core.Entities;

namespace OSIM.Core.Persistence
{
    public class OsimDbContext : DbContext, IDbContext
    {
        public OsimDbContext() : base("MyConnectionString")
        {}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemType>()
                .ToTable("ItemTypes");
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
