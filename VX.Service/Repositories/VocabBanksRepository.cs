using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using VX.Domain;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Factories.Interfaces;
using VX.Service.Infrastructure.Interfaces;
using VX.Service.Repositories.Interfaces;

namespace VX.Service.Repositories
{
    public class VocabBanksRepository : IVocabBanksRepository
    {
        private const string ServiceName = "vocabBanksRepositoy";
        private readonly IServiceSettings serviceSettings;
        private readonly IEntitiesFactory entitiesFactory;
        private readonly ICacheFacade cacheFacade;
        private readonly ICacheKeyFactory cacheKeyFactory;
        
        public VocabBanksRepository(
            IServiceSettings serviceSettings,
            IEntitiesFactory entitiesFactory,
            ICacheFacade cacheFacade,
            ICacheKeyFactory cacheKeyFactory)
        {
            this.serviceSettings = serviceSettings;
            this.entitiesFactory = entitiesFactory;
            this.cacheFacade = cacheFacade;
            this.cacheKeyFactory = cacheKeyFactory;
        }

        public IList<IVocabBank> GetVocabBanks()
        {
            var cacheKey = cacheKeyFactory.BuildKey(ServiceName, string.Empty);
            Func<ObjectSet<VocabBank>, IList<IVocabBank>> retrievingFunction = 
                vocabBanks => vocabBanks
                    .ToList()
                    .Select(entity => entitiesFactory.BuildVocabBank(entity))
                    .ToList();
            
            return GetMultipleBanks(cacheKey, retrievingFunction);
        }

        public IList<IVocabBank> GetVocabBanks(int[] vocabBanksIds)
        {
            var cacheKey = cacheKeyFactory.BuildKey(ServiceName, vocabBanksIds);
            Func<ObjectSet<VocabBank>, IList<IVocabBank>> retrievingFunction =
                vocabBanks => vocabBanks
                                  .Where(bank => vocabBanksIds.Contains(bank.Id))
                                  .ToList()
                                  .Select(entity => entitiesFactory.BuildVocabBank(entity))
                                  .ToList();

            return GetMultipleBanks(cacheKey, retrievingFunction);
        }

        private IList<IVocabBank> GetMultipleBanks(
            string cacheKey, 
            Func<ObjectSet<VocabBank>, IList<IVocabBank>> retrievingFunction)
        {
            IList<IVocabBank> result;
            if (cacheFacade.GetFromCache(cacheKey, out result))
            {
                return result;
            }

            using (var context = new Entities(serviceSettings.ConnectionString))
            {
                result = retrievingFunction(context.VocabBanks);
            }

            cacheFacade.PutIntoCache(result, cacheKey);
            return result;
        }

        public IVocabBank GetVocabBank(int vocabBankId)
        {
            IVocabBank result;
            using(var context = new Entities(serviceSettings.ConnectionString))
            {
                result = context.VocabBanks.Where(item => item.Id == vocabBankId)
                    .Select(entity => entitiesFactory.BuildVocabBank(entity))
                    .FirstOrDefault();
            }
            return result;
        }
    }
}