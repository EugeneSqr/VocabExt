using System.Collections.Generic;
using VX.Domain;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Service.Repositories.Interfaces
{
    public interface IVocabBanksRepository
    {
        IList<IVocabBank> Get();

        IList<IVocabBank> Get(int[] vocabBanksIds);

        IList<IVocabBank> GetListWithoutTranslations();

        IServiceOperationResponse UpdateHeaders(IVocabBank vocabBank);
            
        IServiceOperationResponse DetachTranslation(int vocabBankId, int translationId);

        IServiceOperationResponse AttachTranslation(int vocabBankId, int translationId);
    }
}
