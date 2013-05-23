using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ninject.Modules;
using OSIM.Core.Entities;
using OSIM.Core.Persistence;

namespace OSIM.IntegrationTests
{
    public class IntegrationTestModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<ItemType>>().To<ItemTypeRepository>();
            Bind<IDbContext>().To<OsimDbContext>();
            Bind<IUnitOfWork>().To<SqlUnitOfWork>();
        }
    }
}
