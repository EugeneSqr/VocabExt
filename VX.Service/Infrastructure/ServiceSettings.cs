using System.Configuration;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure
{
    internal class ServiceSettings : IServiceSettings
    {
        public string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["VXEntities"].ConnectionString; }
        }

        public string DomainDatabaseName
        {
            get { return ConfigurationManager.AppSettings["DomainDatabaseName"]; }
        }

        public int DefaultTasksCount
        {
            get { return int.Parse(ConfigurationManager.AppSettings["DefaultTasksCount"]); }
        }

        public int CacheSlidingExpirationSeconds
        {
            get { return int.Parse(ConfigurationManager.AppSettings["CacheSlidingExpirationSeconds"]); }
        }
    }
}