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
    }
}