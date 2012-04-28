using System.Collections.Generic;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Service.Repositories.Interfaces
{
    public interface IVocabBanksRepository
    {
        IList<IVocabBank> GetVocabBanks();

        IVocabBank GetVocabBank(int vocabularyId);
    }
}
