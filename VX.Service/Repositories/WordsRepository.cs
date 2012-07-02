using System.Collections.Generic;
using System.Linq;
using VX.Domain.DataContracts.Interfaces;
using VX.Model;
using VX.Service.Factories.Interfaces;
using VX.Service.Infrastructure.Interfaces;
using VX.Service.Repositories.Interfaces;

namespace VX.Service.Repositories
{
    public class WordsRepository : RepositoryBase, IWordsRepository
    {
        private readonly ISearchStringBuilder searchStringBuilder;

        public WordsRepository(
            IServiceSettings serviceSettings, 
            IEntitiesFactory entitiesFactory, 
            ICacheFacade cacheFacade, 
            ICacheKeyFactory cacheKeyFactory,
            ISearchStringBuilder searchStringBuilder,
            IServiceOperationResponseFactory serviceOperationResponseFactory) 
            : base(serviceSettings, entitiesFactory, cacheFacade, cacheKeyFactory, serviceOperationResponseFactory)
        {
            this.searchStringBuilder = searchStringBuilder;
        }

        public IList<IWord> GetWords(string searchString)
        {
            List<IWord> result;
            var cacheKey = CacheKeyFactory.BuildKey("WordsRepository.GetWords", searchString);
            if (!CacheFacade.GetFromCache(cacheKey, out result))
            {
                var actualSearchString = searchStringBuilder.BuildSearchString(searchString);
                if (!string.IsNullOrEmpty(actualSearchString))
                {
                    using (Entities context = new Entities(ServiceSettings.ConnectionString))
                    {
                        result = context.Words
                            .Where(word => word.Spelling.StartsWith(actualSearchString))
                            .ToList()
                            .Select(item => EntitiesFactory.BuildWord(item))
                            .ToList();
                    }
                }
                else
                {
                    result = new List<IWord>();
                }

                CacheFacade.PutIntoCache(result, cacheKey);
            }
            
            return result;
        }

        public IWord GetWord(int id)
        {
            IWord result;
            var cacheKey = CacheKeyFactory.BuildKey("WordsRepositoy.GetWord", id);
            if (!CacheFacade.GetFromCache(cacheKey, out result))
            {
                using (var context = new Entities(ServiceSettings.ConnectionString))
                {
                    result = EntitiesFactory.BuildWord(
                        context.Words.FirstOrDefault(item => item.Id == id));
                }

                CacheFacade.PutIntoCache(result, cacheKey);
            }

            return result;
        }
    }
}