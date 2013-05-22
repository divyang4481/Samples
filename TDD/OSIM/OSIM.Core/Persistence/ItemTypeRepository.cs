using System;
using OSIM.Core.Entities;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace OSIM.Core.Persistence
{
    public class ItemTypeRepository : IRepository<ItemType>
    {
        public ItemTypeRepository(IDbContext context)
        {
            _context = context;
        }

        private readonly IDbContext _context;

        private IDbSet<ItemType> ItemTypes { get { return _context.Set<ItemType>(); } } 

        public IEnumerable<ItemType> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemType> FindBy(Expression<Func<ItemType, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public ItemType FindById(int id)
        {
            return ItemTypes.Find(id);
        }

        public ItemType Add(ItemType newEntity)
        {
           var result = ItemTypes.Add(newEntity);
            return result;
        }

        public void Remove(ItemType entity)
        {
            throw new NotImplementedException();
        }
    }
}
