using VX.Model;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure.Factories.Context
{
    public class ContextFactory : IContextFactory
    {
        private readonly IServiceSettings serviceSettings;

        public ContextFactory(IServiceSettings serviceSettings)
        {
            this.serviceSettings = serviceSettings;
        }

        public EntitiesContext Build()
        {
            return new EntitiesContext(serviceSettings.ConnectionString);
        }
    }
}