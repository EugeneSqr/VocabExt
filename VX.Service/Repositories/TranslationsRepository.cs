using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using VX.Domain;
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
            IContextFactory contextFactory,
            IEntitiesFactory entitiesFactory, 
            ICacheFacade cacheFacade, 
            ICacheKeyFactory cacheKeyFactory, 
            IServiceOperationResponseFactory serviceOperationResponseFactory, 
            IInputDataConverter inputDataConverter, 
            ITranslationValidator translationValidator) : base(contextFactory, entitiesFactory, cacheFacade, cacheKeyFactory, serviceOperationResponseFactory, inputDataConverter)
        {
            this.translationValidator = translationValidator;
        }

        public IList<ITranslation> GetTranslations(int vocabBankId)
        {
            if (vocabBankId == InputDataConverter.EmptyId)
            {
                return new List<ITranslation>();
            }
            
            var cacheKey = CacheKeyFactory.BuildKey("TranslationsRepository.GetTranslations", vocabBankId);
            List<ITranslation> result;
            if (!CacheFacade.GetFromCache(cacheKey, out result))
            {
                using (var context = ContextFactory.Build())
                {
                    result = context.VocabBanksTranslations
                        .Where(item => item.VocabularyId == vocabBankId)
                        .ToList()
                        .Select(item => EntitiesFactory.BuildTranslation(item.Translation))
                        .ToList();
                }

                CacheFacade.PutIntoCache(result, cacheKey, new [] {"Translations", "VocabBanksTranslations"});
            }

            return result;
        }

        public IServiceOperationResponse SaveTranslation(
            ITranslation translation, 
            out IManyToManyRelationship resultTranslation)
        {
            resultTranslation = null;
            var action = ServiceOperationAction.Update;
            using (var context = ContextFactory.Build())
            {
                var targetTranslation = GetTranslation(translation.Id);
                if (targetTranslation == null)
                {
                    targetTranslation = GetTranslation(translation.Source.Id, translation.Target.Id);
                    if (targetTranslation == null)
                    {
                        targetTranslation = context.Translations.CreateObject<Translation>();
                        action = ServiceOperationAction.Create;
                    }
                }
                
                targetTranslation.SourceId = translation.Source.Id;
                targetTranslation.TargetId = translation.Target.Id;
                
                if (translationValidator.Validate(translation) != ValidationResult.Success)
                {
                    return ServiceOperationResponseFactory.Build(
                        false, 
                        action, 
                        "Validation failed");
                }

                if (targetTranslation.EntityState == EntityState.Detached)
                {
                    context.Translations.AddObject(targetTranslation);
                }

                context.SaveChanges();
                resultTranslation = EntitiesFactory.BuildManyToManyRelationship(
                    targetTranslation.Id, 
                    targetTranslation.SourceId, 
                    targetTranslation.TargetId);
            }

            return ServiceOperationResponseFactory.Build(true, action, "updated successfully");
        }

        private Translation GetTranslation(int translationId)
        {
            if (translationId == InputDataConverter.EmptyId)
            {
                return null;
            }

            var cacheKey = CacheKeyFactory.BuildKey("TranslationRepository.GetTranslationById", translationId);
            Translation result;
            if (!CacheFacade.GetFromCache(cacheKey, out result))
            {
                using (var context = ContextFactory.Build())
                {
                    var translation = context.Translations.FirstOrDefault(item => item.Id == translationId);
                    if (translation != null)
                    {
                        result = translation;
                    }
                }
            }

            return result;
        }

        private Translation GetTranslation(int sourceId, int targetId)
        {
            if (sourceId == InputDataConverter.EmptyId || targetId == InputDataConverter.EmptyId)
            {
                return null;
            }

            var cacheKey = CacheKeyFactory.BuildKey(
                "TranslationRepository.GetTranslationBySourceIdAndTargetId", new[]
                                                                                 {
                                                                                     sourceId, 
                                                                                     targetId
                                                                                 });
            Translation result;
            if (!CacheFacade.GetFromCache(cacheKey, out result))
            {
                using (var context = ContextFactory.Build())
                {
                    var translation = context.Translations.FirstOrDefault(
                        item => item.SourceId == sourceId && item.TargetId == targetId);
                    if (translation != null)
                    {
                        result = translation;
                    }
                }
            }

            return result;
        }
    }
}