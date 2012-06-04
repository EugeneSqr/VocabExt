using System.Collections.Generic;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Service.Infrastructure.Interfaces
{
    public interface ISynonymSelector
    {
        IList<ITranslation> GetSimilarTranslations(ITranslation sourceTranslation,
                                                   IList<ITranslation> targetTranslations);
    }
}