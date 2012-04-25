using System.Collections.Generic;
using System.Linq;
using VX.Domain;
using VX.Domain.Interfaces;
using VX.Domain.Surrogates;

namespace VX.Service.Repositories
{
    public class VocabBanksRepository : IVocabBanksRepository
    {
        private readonly IServiceSettings serviceSettings;
        
        public VocabBanksRepository(IServiceSettings serviceSettings)
        {
            this.serviceSettings = serviceSettings;
        }

        public IList<IVocabBank> GetVocabularies()
        {
            throw new System.NotImplementedException();
        }

        public IVocabBank GetVocabulary(int vocabularyId)
        {
            IVocabBank result;
            using(Entities context = new Entities(serviceSettings.ConnectionString))
            {
                result = context.VocabBanks.Where(item => item.Id == vocabularyId)
                    .Select(bank => new VocabBankSurrogate
                                        {

                                        })
                    .FirstOrDefault();
            }
            return result;
        }
    }
}