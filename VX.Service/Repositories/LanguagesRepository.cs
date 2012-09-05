using System;
using System.Collections.Generic;
using System.Linq;
using VX.Domain.Entities;
using VX.Model;
using VX.Service.Infrastructure.Factories;
using VX.Service.Infrastructure.Factories.CacheKeys;
using VX.Service.Infrastructure.Factories.Context;
using VX.Service.Infrastructure.Factories.Entities;
using VX.Service.Infrastructure.Interfaces;
using VX.Service.Repositories.Interfaces;

namespace VX.Service.Repositories
{
    [RegisterService]
    public class LanguagesRepository : RepositoryBase, ILanguagesRepository
    {
        public LanguagesRepository(
            IContextFactory contextFactory, 
            IAbstractEntitiesFactory factory, 
            ICacheFacade cacheFacade, 
            ICacheKeyFactory cacheKeyFactory) : base(contextFactory, factory, cacheFacade, cacheKeyFactory)
        {
        }

        public ILanguage GetLanguage(int languageId)
        {
            var cacheKey = CacheKeyFactory.BuildKey("LanguageRepository", languageId);
            Func<EntitiesContext, ILanguage> retrievalFunction = context =>
                                                              {
                                                                  var result = Factory.Create
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
            Func<EntitiesContext, IList<ILanguage>> retrievalFunction = context =>
                                                                     {
                                                                         var result = context.Languages.ToList()
                                                                             .Select(
                                                                                 item =>
                                                                                 Factory.Create
                                                                                     <ILanguage, Language>(item)).
                                                                             ToList();
                                                                         CacheFacade.PutIntoCache(result, cacheKey);
                                                                         return result;
                                                                     };
            return Retrieve(retrievalFunction, cacheKey);
        }



        private T Retrieve<T>(Func<EntitiesContext, T> retrievalMethod, string cacheKey)
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