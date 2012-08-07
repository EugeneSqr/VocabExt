using System;
using System.Collections.Generic;
using System.Linq;

using VX.Domain.DataContracts.Interfaces;
using VX.Model;
using VX.Service.Factories.Interfaces;
using VX.Service.Infrastructure.Interfaces;
using VX.Service.Repositories.Interfaces;

namespace VX.Service.Repositories
{
    public class LanguagesRepository : RepositoryBase, ILanguagesRepository
    {
        public LanguagesRepository(
            IContextFactory contextFactory, 
            IEntitiesFactory entitiesFactory, 
            ICacheFacade cacheFacade, 
            ICacheKeyFactory cacheKeyFactory, 
            IServiceOperationResponseFactory serviceOperationResponseFactory, 
            IInputDataConverter inputDataConverter) : base(contextFactory, entitiesFactory, cacheFacade, cacheKeyFactory, serviceOperationResponseFactory, inputDataConverter)
        {
        }

        public ILanguage GetLanguage(int languageId)
        {
            var cacheKey = CacheKeyFactory.BuildKey("LanguageRepository", languageId);
            Func<Entities, ILanguage> retrievalFunction = context =>
                                                              {
                                                                  var result = EntitiesFactory.BuildLanguage(
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
                                                                                 EntitiesFactory.BuildLanguage(item)).
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