using VX.Service.Infrastructure.Factories;
using VX.Service.Infrastructure.Factories.CacheKeys;
using VX.Service.Infrastructure.Factories.Context;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Repositories
{
    public abstract class RepositoryBase
    {
        protected readonly IContextFactory ContextFactory;
        protected readonly IAbstractFactory Factory;
        protected readonly ICacheFacade CacheFacade;
        protected readonly ICacheKeyFactory CacheKeyFactory;

        protected int EmptyId = -1;

        protected RepositoryBase(
            IContextFactory contextFactory,
            IAbstractFactory factory, 
            ICacheFacade cacheFacade, 
            ICacheKeyFactory cacheKeyFactory)
        {
            ContextFactory = contextFactory;
            Factory = factory;
            CacheFacade = cacheFacade;
            CacheKeyFactory = cacheKeyFactory;
        }
    }
}