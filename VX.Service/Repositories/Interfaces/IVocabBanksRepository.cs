using System.Collections.Generic;
using VX.Domain.Entities;

namespace VX.Service.Repositories.Interfaces
{
    public interface IVocabBanksRepository
    {
        string NewVocabBankName { get; }

        IList<IVocabBank> Get();

        IList<IVocabBank> GetWithTranslationsOnly();

        IList<IVocabBank> Get(int[] vocabBanksIds);

        IList<IVocabBank> GetWithTranslationsOnly(int[] vocabBanksIds);

        IList<IVocabBank> GetListWithoutTranslations();

        IVocabBank Create();

        bool Delete(int vocabBankId);

        bool UpdateHeaders(IVocabBank vocabBank);
            
        bool DetachTranslation(int vocabBankId, int translationId);

        bool AttachTranslation(int vocabBankId, int translationId);
    }
}
