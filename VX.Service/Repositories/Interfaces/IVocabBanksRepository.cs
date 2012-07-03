using System.Collections.Generic;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Service.Repositories.Interfaces
{
    public interface IVocabBanksRepository
    {
        IList<IVocabBank> GetVocabBanks();

        IList<IVocabBank> GetVocabBanks(int[] vocabBanksIds);

        IList<IVocabBank> GetVocabBanksList();

        IServiceOperationResponse DetachTranslation(int vocabBankId, int translationId);
    }
}
