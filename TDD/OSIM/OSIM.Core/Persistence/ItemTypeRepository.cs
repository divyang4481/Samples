using System;
using OSIM.Core.Entities;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace OSIM.Core.Persistence
{
    public class ItemTypeRepository : IRepository<ItemType>
    {
        private readonly IDbContext _context;

        public ItemTypeRepository(IDbContext context)
        {
            _context = context;
        }

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
            throw new NotImplementedException();
        }

        public ItemType Add(ItemType newEntity)
        {
           var result = _context.Set<ItemType>().Add(newEntity);
            return result;
        }

        public void Remove(ItemType entity)
        {
            throw new NotImplementedException();
        }
    }
}
