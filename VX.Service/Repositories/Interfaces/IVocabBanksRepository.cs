using System.Collections.Generic;
using VX.Domain.Entities;
using VX.Domain.Surrogates;

namespace VX.Service.Repositories.Interfaces
{
    public interface IVocabBanksRepository
    {
        string NewVocabBankName { get; }

        IList<IVocabBank> Get();

        IList<IVocabBank> GetWithTranslationsOnly();

        IList<IVocabBank> Get(int[] vocabBanksIds);

        IList<IVocabBank> GetWithTranslationsOnly(int[] vocabBanksIds);

        IList<IVocabBankSummary> GetSummary();

        IVocabBank Create();

        bool Delete(int vocabBankId);

        bool UpdateSummary(IVocabBankSummary vocabBankSummary);
            
        bool DetachTranslation(int vocabBankId, int translationId);

        void AttachTranslation(int vocabBankId, int translationId);
    }
}
