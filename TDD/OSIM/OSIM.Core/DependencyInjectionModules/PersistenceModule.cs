using Ninject.Modules;
using OSIM.Core.Entities;
using OSIM.Core.Persistence;

namespace OSIM.Core.DependencyInjectionModules
{
    public class PersistenceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}
