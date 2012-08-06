using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Globalization;
using System.Linq;
using VX.Domain;
using VX.Domain.DataContracts.Interfaces;
using VX.Model;
using VX.Service.Factories.Interfaces;
using VX.Service.Infrastructure.Interfaces;
using VX.Service.Repositories.Interfaces;

namespace VX.Service.Repositories
{
    public class VocabBanksRepository : RepositoryBase, IVocabBanksRepository
    {
        private const string ServiceName = "vocabBanksRepositoy";
        private const string TagsQueryPath = "VocabBanksTags.Tag";

        public VocabBanksRepository(
            IServiceSettings serviceSettings, 
            IEntitiesFactory entitiesFactory, 
            ICacheFacade cacheFacade, 
            ICacheKeyFactory cacheKeyFactory,
            IServiceOperationResponseFactory serviceOperationResponseFactory,
            IInputDataConverter inputDataConverter) 
            : base(
                serviceSettings, 
                entitiesFactory, 
                cacheFacade, 
                cacheKeyFactory, 
                serviceOperationResponseFactory, 
                inputDataConverter)
        {
        }

        public string NewVocabBankName
        {
            get { return "New Vocab Bank"; }
        }

        public IList<IVocabBank> Get()
        {
            var cacheKey = CacheKeyFactory.BuildKey(ServiceName, string.Empty);
            Func<ObjectSet<VocabBank>, IList<IVocabBank>> retrievingFunction = 
                vocabBanks => vocabBanks
                    .ToList()
                    .Select(entity => EntitiesFactory.BuildVocabBank(entity))
                    .ToList();
            
            return GetMultipleBanks(cacheKey, retrievingFunction, true);
        }

        public IList<IVocabBank> Get(int[] vocabBanksIds)
        {
            var cacheKey = CacheKeyFactory.BuildKey(ServiceName, vocabBanksIds);
            Func<ObjectSet<VocabBank>, IList<IVocabBank>> retrievingFunction =
                vocabBanks => vocabBanks
                                  .Where(bank => vocabBanksIds.Contains(bank.Id))
                                  .ToList()
                                  .Select(entity => EntitiesFactory.BuildVocabBank(entity))
                                  .ToList();

            return GetMultipleBanks(cacheKey, retrievingFunction, true);
        }

        public IList<IVocabBank> GetListWithoutTranslations()
        {
            var cacheKey = CacheKeyFactory.BuildKey(ServiceName, string.Empty);
            Func<ObjectSet<VocabBank>, IList<IVocabBank>> retrievingFunction =
                vocabBanks => vocabBanks.Include(TagsQueryPath)
                    .ToList()
                    .Select(bank => EntitiesFactory.BuildVocabBank(bank))
                    .ToList();

            return GetMultipleBanks(cacheKey, retrievingFunction, false);
        }

        public IVocabBank Create()
        {
            IVocabBank result;
            using (var context = new Entities(ServiceSettings.ConnectionString))
            {
                var newVocabBank = context.VocabBanks.CreateObject();
                newVocabBank.Name = NewVocabBankName;
                context.VocabBanks.AddObject(newVocabBank);
                context.SaveChanges();
                result = EntitiesFactory.BuildVocabBank(newVocabBank);
            }

            return result;
        }

        public IServiceOperationResponse Delete(int vocabBankId)
        {
            IServiceOperationResponse result;
            using (var context = new Entities(ServiceSettings.ConnectionString))
            {
                var bankToDelete = context.VocabBanks.FirstOrDefault(item => item.Id == vocabBankId);
                if (bankToDelete != null)
                {
                    context.VocabBanks.DeleteObject(bankToDelete);
                    context.SaveChanges();
                    result = ServiceOperationResponseFactory.Build(
                        true, 
                        ServiceOperationAction.Delete, 
                        vocabBankId.ToString(CultureInfo.InvariantCulture));
                }
                else
                {
                    result = ServiceOperationResponseFactory.Build(
                    false,
                    ServiceOperationAction.Delete,
                    "Vocabulary bank is not found");
                }
            }

            return result;
        }

        public IServiceOperationResponse UpdateHeaders(IVocabBank vocabBank)
        {
            using (var context = new Entities(ServiceSettings.ConnectionString))
            {
                var bankToUpdate = context.VocabBanks.FirstOrDefault(bank => bank.Id == vocabBank.Id);
                if (bankToUpdate != null)
                {
                    bankToUpdate.Name = vocabBank.Name;
                    bankToUpdate.Description = vocabBank.Description;
                    context.SaveChanges();
                }
            }

            return ServiceOperationResponseFactory.Build(true, ServiceOperationAction.Update);
        }

        public IServiceOperationResponse DetachTranslation(int vocabBankId, int translationId)
        {
            using (var context = new Entities(ServiceSettings.ConnectionString))
            {
                try
                {
                    context.VocabBanksTranslations.DeleteObject(
                        context.VocabBanksTranslations.FirstOrDefault(
                            item => item.VocabularyId == vocabBankId && item.TranslationId == translationId));
                    context.SaveChanges();
                }
                catch(Exception)
                {
                    return ServiceOperationResponseFactory.Build(
                        false, 
                        ServiceOperationAction.Detach, 
                        "item not found");
                }

                return ServiceOperationResponseFactory.Build(
                    true, 
                    ServiceOperationAction.Detach, 
                    "detached successfully");
            }
        }

        public IServiceOperationResponse AttachTranslation(int vocabBankId, int translationId)
        {
            var action = ServiceOperationAction.None;
            using (var context = new Entities(ServiceSettings.ConnectionString))
            {
                var translation =
                    context.VocabBanksTranslations.FirstOrDefault(
                        item => item.VocabularyId == vocabBankId && item.TranslationId == translationId);
                if (translation == null)
                {
                    action = ServiceOperationAction.Attach;
                    translation = context.VocabBanksTranslations.CreateObject<VocabBanksTranslation>();
                    translation.VocabularyId = vocabBankId;
                    translation.TranslationId = translationId;
                    context.VocabBanksTranslations.AddObject(translation);
                    context.SaveChanges();
                }
            }

            return ServiceOperationResponseFactory.Build(true, action);
        }

        private IList<IVocabBank> GetMultipleBanks(
            string cacheKey, 
            Func<ObjectSet<VocabBank>, IList<IVocabBank>> retrievingFunction,
            bool useLazyLoading)
        {
            IList<IVocabBank> result;
            if (!CacheFacade.GetFromCache(cacheKey, out result))
            {
                using (var context = new Entities(ServiceSettings.ConnectionString))
                {
                    context.ContextOptions.LazyLoadingEnabled = useLazyLoading;
                    result = retrievingFunction(context.VocabBanks);
                }

                CacheFacade.PutIntoCache(result, cacheKey, new[] { "VocabBanks" });
            }
            
            return result;
        }
    }
}