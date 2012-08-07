using VX.Service.Factories.Interfaces;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Repositories
{
    public abstract class RepositoryBase
    {
        protected readonly IContextFactory ContextFactory;
        protected readonly IEntitiesFactory EntitiesFactory;
        protected readonly ICacheFacade CacheFacade;
        protected readonly ICacheKeyFactory CacheKeyFactory;
        protected readonly IServiceOperationResponseFactory ServiceOperationResponseFactory;
        protected readonly IInputDataConverter InputDataConverter;

        protected RepositoryBase(
            IContextFactory contextFactory,
            IEntitiesFactory entitiesFactory, 
            ICacheFacade cacheFacade, 
            ICacheKeyFactory cacheKeyFactory,
            IServiceOperationResponseFactory serviceOperationResponseFactory,
            IInputDataConverter inputDataConverter)
        {
            ContextFactory = contextFactory;
            EntitiesFactory = entitiesFactory;
            CacheFacade = cacheFacade;
            CacheKeyFactory = cacheKeyFactory;
            ServiceOperationResponseFactory = serviceOperationResponseFactory;
            InputDataConverter = inputDataConverter;
        }
    }
}