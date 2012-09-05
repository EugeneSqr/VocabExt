using System.Collections.Generic;
using VX.Domain.Entities;
using VX.Domain.Surrogates;

namespace VX.Service.Repositories.Interfaces
{
    public interface ITranslationsRepository
    {
        IList<ITranslation> GetTranslations(int vocabBankId);

        bool SaveTranslation(
            ITranslation translation, 
            out IManyToManyRelationship resultTranslation,
            out ServiceOperationAction action);
    }
}