using System.Collections.Generic;
using VX.Domain.Interfaces.Entities;

namespace VX.Domain.Interfaces.Repositories
{
    public interface IVocabBanksRepository
    {
        IList<IVocabBank> GetVocabBanks();

        IVocabBank GetVocabBank(int vocabularyId);
    }
}
