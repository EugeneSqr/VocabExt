using System.Configuration;
using VX.Service.Interfaces;

namespace VX.Service
{
    internal class ServiceSettings : IServiceSettings
    {
        public string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["VXEntities"].ConnectionString; }
        }
    }
}