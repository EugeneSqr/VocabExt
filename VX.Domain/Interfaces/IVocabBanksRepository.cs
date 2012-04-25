using System.Collections.Generic;

namespace VX.Domain.Interfaces
{
    public interface IVocabBanksRepository
    {
        IList<IVocabBank> GetVocabBanks();

        IVocabBank GetVocabBank(int vocabularyId);
    }
}
