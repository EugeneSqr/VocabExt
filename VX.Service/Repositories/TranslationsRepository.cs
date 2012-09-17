using System.Collections.Generic;
using System.Linq;
using VX.Domain.Entities;
using VX.Domain.Surrogates;
using VX.Domain.Surrogates.Impl;
using VX.Model;
using VX.Service.Infrastructure.Exceptions;
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
            IAbstractEntitiesFactory entitiesFactory, 
            ICacheFacade cacheFacade, 
            ICacheKeyFactory cacheKeyFactory, 
            ITranslationValidator translationValidator) : base(contextFactory, entitiesFactory, cacheFacade, cacheKeyFactory)
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
                        .Select(item => EntitiesFactory.Create<ITranslation, Translation>(item.Translation))
                        .ToList();
                }

                CacheFacade.PutIntoCache(result, cacheKey, new [] {"Translations", "VocabBanksTranslations"});
            }

            return result;
        }

        public IManyToManyRelationship SaveTranslation(
            ITranslation translation, 
            out ServiceOperationAction action)
        {
            action = ServiceOperationAction.Update;
            if (!translationValidator.Validate(translation).Status)
            {
                throw new ValidationFailedException();
            }

            using (var context = ContextFactory.Build())
            {
                var targetTranslation = GetTranslation(translation.Id) ??
                                        GetTranslation(translation.Source.Id, translation.Target.Id);

                if (targetTranslation == null)
                {
                    targetTranslation = context.Translations.CreateObject<Translation>();
                    context.Translations.AddObject(targetTranslation);
                    action = ServiceOperationAction.Create;
                }
                else
                {
                    context.Attach(targetTranslation);
                    action = ServiceOperationAction.Update;
                }
                
                targetTranslation.Source = context.Words.Single(item => item.Id == translation.Source.Id);
                targetTranslation.Target = context.Words.Single(item => item.Id == translation.Target.Id);
                
                context.SaveChanges();
                // TODO: to factory
                return new ManyToManyRelationship
                                        {
                                            Id = targetTranslation.Id,
                                            SourceId = targetTranslation.SourceId,
                                            TargetId = targetTranslation.TargetId
                                        };
            }
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