using System.Collections.Generic;
using VX.Domain;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Service.Repositories.Interfaces
{
    public interface ITranslationsRepository
    {
        IList<ITranslation> GetTranslations(int vocabBankId);

        IServiceOperationResponse SaveTranslation(ITranslation translation, out IManyToManyRelationship resultTranslation);
    }
}