using System.Configuration;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure
{
    internal class ServiceSettings : IServiceSettings
    {
        private const string DefaultTasksCountKey = "DefaultTasksCount";
        
        public string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["VXEntities"].ConnectionString; }
        }

        public int DefaultTasksCount
        {
            get { return int.Parse(ConfigurationManager.AppSettings[DefaultTasksCountKey]); }
        }
    }
}