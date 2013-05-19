using Ninject;

namespace BusinessApplication.Core
{
    public interface IBusinessService
    {
        ILogger Logger { get; set; }
    }

    public class BusinessService : IBusinessService
    {
        [Inject]
        public ILogger Logger { get; set; }

        private IDataAccessComponent dataAccessComponent { get; set; }

       public BusinessService(IDataAccessComponent dataAccessComponent)
       {
           this.dataAccessComponent = dataAccessComponent;
       }
    }
}
