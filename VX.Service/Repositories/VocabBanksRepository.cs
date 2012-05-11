using System.Collections.Generic;
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
        private readonly IServiceSettings serviceSettings;
        private readonly IEntitiesFactory entitiesFactory;
        private readonly ICacheFacade cacheFacade;
        
        public VocabBanksRepository(
            IServiceSettings serviceSettings,
            IEntitiesFactory entitiesFactory,
            ICacheFacade cacheFacade)
        {
            this.serviceSettings = serviceSettings;
            this.entitiesFactory = entitiesFactory;
            this.cacheFacade = cacheFacade;
        }

        public IList<IVocabBank> GetVocabBanks()
        {
            IList<IVocabBank> result;
            if (cacheFacade.GetFromCache("", out result))
            {
                return result;
            }

            using (var context = new Entities(serviceSettings.ConnectionString))
            {
                result = context.VocabBanks
                    .ToList()
                    .Select(entity => entitiesFactory.BuildVocabBank(entity))
                    .ToList();
            }

            cacheFacade.PutIntoCache(result, "");
            return result;
        }

        public IVocabBank GetVocabBank(int vocabularyId)
        {
            IVocabBank result;
            using(var context = new Entities(serviceSettings.ConnectionString))
            {
                result = context.VocabBanks.Where(item => item.Id == vocabularyId)
                    .Select(entity => entitiesFactory.BuildVocabBank(entity))
                    .FirstOrDefault();
            }
            return result;
        }
    }
}