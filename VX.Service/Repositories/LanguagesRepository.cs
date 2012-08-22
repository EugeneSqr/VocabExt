using System;
using System.Collections.Generic;
using System.Linq;

using VX.Domain.DataContracts.Interfaces;
using VX.Model;
using VX.Service.Infrastructure.Factories.Adapters;
using VX.Service.Infrastructure.Factories.CacheKeys;
using VX.Service.Infrastructure.Factories.EntitiesContext;
using VX.Service.Infrastructure.Factories.ServiceOperationResponses;
using VX.Service.Infrastructure.Interfaces;
using VX.Service.Repositories.Interfaces;

namespace VX.Service.Repositories
{
    public class LanguagesRepository : RepositoryBase, ILanguagesRepository
    {
        public LanguagesRepository(
            IContextFactory contextFactory, 
            IAdapterFactory entitiesFactory, 
            ICacheFacade cacheFacade, 
            ICacheKeyFactory cacheKeyFactory, 
            IServiceOperationResponseFactory serviceOperationResponseFactory) : base(contextFactory, entitiesFactory, cacheFacade, cacheKeyFactory, serviceOperationResponseFactory)
        {
        }

        public ILanguage GetLanguage(int languageId)
        {
            var cacheKey = CacheKeyFactory.BuildKey("LanguageRepository", languageId);
            Func<Entities, ILanguage> retrievalFunction = context =>
                                                              {
                                                                  var result = EntitiesFactory.Create
                                                                      <ILanguage, Language>(
                                                                          context.Languages.FirstOrDefault(
                                                                              lang => lang.Id == languageId));
                                                                  CacheFacade.PutIntoCache(result, cacheKey);
                                                                  return result;
                                                              };
            return Retrieve(retrievalFunction, cacheKey);
        }

        public IList<ILanguage> GetLanguages()
        {
            var cacheKey = CacheKeyFactory.BuildKey("LanguageRepository", "AllLanguages");
            Func<Entities, IList<ILanguage>> retrievalFunction = context =>
                                                                     {
                                                                         var result = context.Languages.ToList()
                                                                             .Select(
                                                                                 item =>
                                                                                 EntitiesFactory.Create
                                                                                     <ILanguage, Language>(item)).
                                                                             ToList();
                                                                         CacheFacade.PutIntoCache(result, cacheKey);
                                                                         return result;
                                                                     };
            return Retrieve(retrievalFunction, cacheKey);
        }



        private T Retrieve<T>(Func<Entities, T> retrievalMethod, string cacheKey)
        {
            T result;
            if (!CacheFacade.GetFromCache(cacheKey, out result))
            {
                using (var context = ContextFactory.Build())
                {
                    result = retrievalMethod(context);
                    CacheFacade.PutIntoCache(result, cacheKey);
                }
            }

            return result;
        }
    }
}