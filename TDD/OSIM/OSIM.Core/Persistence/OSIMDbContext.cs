using System.Data.Entity;
using OSIM.Core.Entities;

namespace OSIM.Core.Persistence
{
    public class OSIMDbContext : DbContext
    {
        public DbSet<ItemType> ItemTypes { get; set; }
    }
}
