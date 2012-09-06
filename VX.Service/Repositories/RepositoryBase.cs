using VX.Service.Infrastructure.Factories.CacheKeys;
using VX.Service.Infrastructure.Factories.Context;
using VX.Service.Infrastructure.Factories.Entities;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Repositories
{
    public abstract class RepositoryBase
    {
        protected readonly IContextFactory ContextFactory;
        protected readonly IAbstractEntitiesFactory EntitiesFactory;
        protected readonly ICacheFacade CacheFacade;
        protected readonly ICacheKeyFactory CacheKeyFactory;

        protected int EmptyId;

        protected RepositoryBase(
            IContextFactory contextFactory,
            IAbstractEntitiesFactory entitiesFactory,
            ICacheFacade cacheFacade, 
            ICacheKeyFactory cacheKeyFactory)
        {
            ContextFactory = contextFactory;
            EntitiesFactory = entitiesFactory;
            CacheFacade = cacheFacade;
            CacheKeyFactory = cacheKeyFactory;
        }
    }
}