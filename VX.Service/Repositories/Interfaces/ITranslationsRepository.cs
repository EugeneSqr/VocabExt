using System.Collections.Generic;
using VX.Domain.Entities;
using VX.Domain.Surrogates;

namespace VX.Service.Repositories.Interfaces
{
    public interface ITranslationsRepository
    {
        IList<ITranslation> GetTranslations(int vocabBankId);

        IManyToManyRelationship SaveTranslation(
            ITranslation translation, 
            out ServiceOperationAction action);
    }
}