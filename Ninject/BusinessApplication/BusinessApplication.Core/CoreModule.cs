using Ninject.Modules;

namespace BusinessApplication.Core
{
    public class CoreModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBusinessService>().To<BusinessService>();
            Bind<ILogger>().To<Logger>();
            Bind<IDataAccessComponent>().ToProvider<DataAccessComponentProvider>();

            Bind<IPersonRepository>().To<PersonRepository>();
            Bind<IPersonService>().To<PersonService>();
        }
    }
}
