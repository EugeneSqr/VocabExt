using System.Collections.Generic;
using System.Linq;
using VX.Domain.DataContracts.Interfaces;
using VX.Model;
using VX.Service.Factories.Interfaces;
using VX.Service.Infrastructure.Interfaces;
using VX.Service.Repositories.Interfaces;

namespace VX.Service.Repositories
{
    public class TranslationsRepository : RepositoryBase, ITranslationsRepository
    {
        public TranslationsRepository(
            IServiceSettings serviceSettings, 
            IEntitiesFactory entitiesFactory, 
            ICacheFacade cacheFacade, 
            ICacheKeyFactory cacheKeyFactory) : base(serviceSettings, entitiesFactory, cacheFacade, cacheKeyFactory)
        {
        }

        public IList<ITranslation> GetTranslations(string vocabBankId)
        {
            var cacheKey = CacheKeyFactory.BuildKey("TranslationsRepository", vocabBankId);
            List<ITranslation> result;
            if (!CacheFacade.GetFromCache(cacheKey, out result))
            {
                int vocabularyId;
                if (int.TryParse(vocabBankId, out vocabularyId))
                {
                    using (var context = new Entities(ServiceSettings.ConnectionString))
                    {
                        result = context.VocabBanksTranslations
                            .Where(item => item.VocabularyId == vocabularyId)
                            .ToList()
                            .Select(item => EntitiesFactory.BuildTranslation(item.Translation))
                            .ToList();
                    }
                }
                else
                {
                    result = new List<ITranslation>();
                }

                CacheFacade.PutIntoCache(result, cacheKey);
            }

            return result;
        }

        public bool UpdateTranslation(ITranslation translation)
        {
            using (var context = new Entities(ServiceSettings.ConnectionString))
            {
                var targetTranslation = context.Translations.First(item => item.Id == translation.Id);
                targetTranslation.SourceId = translation.Source.Id;
                targetTranslation.TargetId = translation.Target.Id;
                context.SaveChanges();
            }

            return true;
        }
    }
}