using System.Collections.Generic;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Service.Repositories.Interfaces
{
    public interface ITranslationsRepository
    {
        IList<ITranslation> GetTranslations(string vocabBankId);

        bool UpdateTranslation(ITranslation translation);
    }
}