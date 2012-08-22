using VX.Service.Infrastructure.Factories.Adapters;
using VX.Service.Infrastructure.Factories.CacheKeys;
using VX.Service.Infrastructure.Factories.EntitiesContext;
using VX.Service.Infrastructure.Factories.ServiceOperationResponses;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Repositories
{
    public abstract class RepositoryBase
    {
        protected readonly IContextFactory ContextFactory;
        protected readonly IAdapterFactory EntitiesFactory;
        protected readonly ICacheFacade CacheFacade;
        protected readonly ICacheKeyFactory CacheKeyFactory;
        protected readonly IServiceOperationResponseFactory ServiceOperationResponseFactory;

        protected int EmptyId = -1;

        protected RepositoryBase(
            IContextFactory contextFactory,
            IAdapterFactory entitiesFactory, 
            ICacheFacade cacheFacade, 
            ICacheKeyFactory cacheKeyFactory,
            IServiceOperationResponseFactory serviceOperationResponseFactory)
        {
            ContextFactory = contextFactory;
            EntitiesFactory = entitiesFactory;
            CacheFacade = cacheFacade;
            CacheKeyFactory = cacheKeyFactory;
            ServiceOperationResponseFactory = serviceOperationResponseFactory;
        }
    }
}