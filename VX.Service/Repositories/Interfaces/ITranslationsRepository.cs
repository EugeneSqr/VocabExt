using System.Collections.Generic;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Service.Repositories.Interfaces
{
    public interface ITranslationsRepository
    {
        IList<ITranslation> GetTranslations(int vocabBankId);

        IServiceOperationResponse UpdateTranslation(ITranslation translation);

        IServiceOperationResponse DeleteTranslation(int translationId);
    }
}