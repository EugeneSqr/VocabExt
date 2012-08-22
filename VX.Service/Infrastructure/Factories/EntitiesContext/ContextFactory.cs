using VX.Model;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure.Factories.EntitiesContext
{
    public class ContextFactory : IContextFactory
    {
        private readonly IServiceSettings serviceSettings;

        public ContextFactory(IServiceSettings serviceSettings)
        {
            this.serviceSettings = serviceSettings;
        }

        public Entities Build()
        {
            return new Entities(serviceSettings.ConnectionString);
        }
    }
}