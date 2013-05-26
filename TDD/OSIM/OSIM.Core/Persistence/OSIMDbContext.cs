using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using OSIM.Core.Entities;

namespace OSIM.Core.Persistence
{
    public class OsimDbContext : DbContext
    {
        public OsimDbContext()
        {}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<ItemType> ItemTypes { get; set; }
    }
}
