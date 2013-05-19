namespace BusinessApplication.Core
{
    public interface IDataAccessComponent
    {   
    }

    public class DataAccessComponent : IDataAccessComponent
    {
        private string dbConnectionString;

        public DataAccessComponent(string dbConnectionString)
        {
            this.dbConnectionString = dbConnectionString;
        }
    }
}
