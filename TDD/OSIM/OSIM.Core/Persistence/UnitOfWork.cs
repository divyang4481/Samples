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
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private OsimDbContext _dbContext = new OsimDbContext();
        private GenericRepository<ItemType> _itemTypeRepository;

        public GenericRepository<ItemType> ItemTypes
        {
            get
            {
                if (this._itemTypeRepository == null)
                {
                    this._itemTypeRepository = new GenericRepository<ItemType>(_dbContext);
                }
                return _itemTypeRepository;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
