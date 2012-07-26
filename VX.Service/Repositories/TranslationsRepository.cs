using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using VX.Domain.DataContracts.Interfaces;
using VX.Model;
using VX.Service.CompositeValidators.Interfaces;
using VX.Service.Factories.Interfaces;
using VX.Service.Infrastructure.Interfaces;
using VX.Service.Repositories.Interfaces;

namespace VX.Service.Repositories
{
    public class TranslationsRepository : RepositoryBase, ITranslationsRepository
    {
        private readonly ITranslationValidator translationValidator;

        public TranslationsRepository(
            IServiceSettings serviceSettings, 
            IEntitiesFactory entitiesFactory, 
            ICacheFacade cacheFacade, 
            ICacheKeyFactory cacheKeyFactory,
            ITranslationValidator translationValidator,
            IServiceOperationResponseFactory serviceOperationResponseFactory,
            IInputDataConverter inputDataConverter) 
            : base(serviceSettings, entitiesFactory, cacheFacade, cacheKeyFactory, serviceOperationResponseFactory, inputDataConverter)
        {
            this.translationValidator = translationValidator;
        }
        
        public IList<ITranslation> GetTranslations(int vocabBankId)
        {
            var cacheKey = CacheKeyFactory.BuildKey("TranslationsRepository", vocabBankId);
            List<ITranslation> result;
            if (!CacheFacade.GetFromCache(cacheKey, out result))
            {
                if (vocabBankId != InputDataConverter.EmptyId)
                {
                    using (var context = new Entities(ServiceSettings.ConnectionString))
                    {
                        result = context.VocabBanksTranslations
                            .Where(item => item.VocabularyId == vocabBankId)
                            .ToList()
                            .Select(item => EntitiesFactory.BuildTranslation(item.Translation))
                            .ToList();
                    }
                }
                else
                {
                    result = new List<ITranslation>();
                }

                CacheFacade.PutIntoCache(result, cacheKey, new [] {"Translations", "VocabBanksTranslations"});
            }

            return result;
        }
        
        public IServiceOperationResponse SaveTranslation(ITranslation translation, int vocabBankId)
        {
            if (translationValidator.Validate(translation) != ValidationResult.Success)
            {
                return ServiceOperationResponseFactory.Build(false, "Validation failed");
            }

            using (var context = new Entities(ServiceSettings.ConnectionString))
            {
                var targetTranslation = context.Translations.First(item => item.Id == translation.Id);
                targetTranslation.SourceId = translation.Source.Id;
                targetTranslation.TargetId = translation.Target.Id;
                context.SaveChanges();
            }

            return ServiceOperationResponseFactory.Build(true, string.Empty);
        }
    }
}