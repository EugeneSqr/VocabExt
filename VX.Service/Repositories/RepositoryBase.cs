using VX.Service.Factories.Interfaces;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Repositories
{
    public abstract class RepositoryBase
    {
        protected readonly IServiceSettings ServiceSettings;
        protected readonly IEntitiesFactory EntitiesFactory;
        protected readonly ICacheFacade CacheFacade;
        protected readonly ICacheKeyFactory CacheKeyFactory;
        protected readonly IServiceOperationResponseFactory ServiceOperationResponseFactory;
        protected readonly IInputDataConverter InputDataConverter;

        protected RepositoryBase(
            IServiceSettings serviceSettings, 
            IEntitiesFactory entitiesFactory, 
            ICacheFacade cacheFacade, 
            ICacheKeyFactory cacheKeyFactory,
            IServiceOperationResponseFactory serviceOperationResponseFactory,
            IInputDataConverter inputDataConverter)
        {
            ServiceSettings = serviceSettings;
            EntitiesFactory = entitiesFactory;
            CacheFacade = cacheFacade;
            CacheKeyFactory = cacheKeyFactory;
            ServiceOperationResponseFactory = serviceOperationResponseFactory;
            InputDataConverter = inputDataConverter;
        }
    }
}