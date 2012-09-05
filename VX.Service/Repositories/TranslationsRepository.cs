using System.Collections.Generic;
using System.Data;
using System.Linq;
using VX.Domain.Entities;
using VX.Domain.Surrogates;
using VX.Domain.Surrogates.Impl;
using VX.Model;
using VX.Service.Infrastructure.Factories;
using VX.Service.Infrastructure.Factories.CacheKeys;
using VX.Service.Infrastructure.Factories.Context;
using VX.Service.Infrastructure.Factories.Entities;
using VX.Service.Infrastructure.Interfaces;
using VX.Service.Repositories.Interfaces;
using VX.Service.Validators.Interfaces;

namespace VX.Service.Repositories
{
    [RegisterService]
    public class TranslationsRepository : RepositoryBase, ITranslationsRepository
    {
        private readonly ITranslationValidator translationValidator;

        public TranslationsRepository(
            IContextFactory contextFactory, 
            IAbstractEntitiesFactory factory, 
            ICacheFacade cacheFacade, 
            ICacheKeyFactory cacheKeyFactory, 
            ITranslationValidator translationValidator) : base(contextFactory, factory, cacheFacade, cacheKeyFactory)
        {
            this.translationValidator = translationValidator;
        }

        public IList<ITranslation> GetTranslations(int vocabBankId)
        {
            if (vocabBankId == EmptyId)
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
                        .Select(item => Factory.Create<ITranslation, Translation>(item.Translation))
                        .ToList();
                }

                CacheFacade.PutIntoCache(result, cacheKey, new [] {"Translations", "VocabBanksTranslations"});
            }

            return result;
        }

        public bool SaveTranslation(
            ITranslation translation, 
            out IManyToManyRelationship resultTranslation, 
            out ServiceOperationAction action)
        {
            resultTranslation = null;
            action = ServiceOperationAction.Update;
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
                
                if (!translationValidator.Validate(translation).Status)
                {
                    return false;
                }

                if (targetTranslation.EntityState == EntityState.Detached)
                {
                    context.Translations.AddObject(targetTranslation);
                }

                context.SaveChanges();
                // TODO: to factory
                resultTranslation = new ManyToManyRelationship
                                        {
                                            Id = targetTranslation.Id,
                                            SourceId = targetTranslation.SourceId,
                                            TargetId = targetTranslation.TargetId
                                        };
            }

            return true;
        }

        private Translation GetTranslation(int translationId)
        {
            if (translationId == EmptyId)
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
            if (sourceId == EmptyId || targetId == EmptyId)
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