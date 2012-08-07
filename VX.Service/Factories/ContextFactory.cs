using VX.Model;
using VX.Service.Factories.Interfaces;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Factories
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