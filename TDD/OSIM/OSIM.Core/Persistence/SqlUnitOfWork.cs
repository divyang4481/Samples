using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSIM.Core.Entities;

namespace OSIM.Core.Persistence
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _context;
        private readonly IRepository<ItemType> _itemTypeRepository;

        public SqlUnitOfWork(IDbContext context, IRepository<ItemType> itemTypeRepository)
        {
            _context = context;
            _itemTypeRepository = itemTypeRepository;
        }

        public IRepository<ItemType> ItemTypes
        {
            get { return _itemTypeRepository; }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
