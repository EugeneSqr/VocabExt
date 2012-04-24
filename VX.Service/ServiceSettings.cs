using System.Configuration;

namespace VX.Service
{
    public class ServiceSettings : IServiceSettings
    {
        public string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["VXEntities"].ConnectionString; }
        }
    }
}