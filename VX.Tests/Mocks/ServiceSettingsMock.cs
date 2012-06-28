using VX.Service.Infrastructure.Interfaces;

namespace VX.Tests.Mocks
{
    public class ServiceSettingsMock : IServiceSettings
    {
        public string ConnectionString
        {
            get { return "metadata=res://*/VocabModel.csdl|res://*/VocabModel.ssdl|res://*/VocabModel.msl;provider=System.Data.SqlClient;provider connection string=\"Data Source=.;Initial Catalog=VXTest;User ID=sa;Password=sa;MultipleActiveResultSets=True\""; }
        }

        public string DomainDatabaseName
        {
            get { return "VX"; }
        }

        public int DefaultTasksCount
        {
            get { return 10; }
        }

        public int CacheSlidingExpirationSeconds
        {
            get { return 300; }
        }
    }
}
