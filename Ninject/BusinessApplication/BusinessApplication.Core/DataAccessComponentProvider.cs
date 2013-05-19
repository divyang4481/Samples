using Ninject.Activation;
using System.Configuration;

namespace BusinessApplication.Core
{
    public class DataAccessComponentProvider : Provider<IDataAccessComponent>
    {
        protected override IDataAccessComponent CreateInstance(IContext context)
        {
            var dbConnectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            return new DataAccessComponent(dbConnectionString);
        }
    }
}
