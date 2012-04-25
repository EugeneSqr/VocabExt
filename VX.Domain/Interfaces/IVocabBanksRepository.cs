using System.Collections.Generic;

namespace VX.Domain.Interfaces
{
    public interface IVocabBanksRepository
    {
        IList<IVocabBank> GetVocabularies();

        IVocabBank GetVocabulary(int vocabularyId);
    }
}
