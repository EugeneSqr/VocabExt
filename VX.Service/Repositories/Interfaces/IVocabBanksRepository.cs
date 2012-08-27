using System.Collections.Generic;
using VX.Domain;
using VX.Domain.DataContracts.Interfaces;

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

        IServiceOperationResponse Delete(int vocabBankId);

        IServiceOperationResponse UpdateHeaders(IVocabBank vocabBank);
            
        IServiceOperationResponse DetachTranslation(int vocabBankId, int translationId);

        IServiceOperationResponse AttachTranslation(int vocabBankId, int translationId);
    }
}
