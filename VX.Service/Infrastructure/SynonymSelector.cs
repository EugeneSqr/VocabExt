using System.Collections.Generic;
using System.Linq;
using VX.Domain.Entities;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure
{
    [RegisterService]
    public class SynonymSelector : ISynonymSelector
    {
        public IList<ITranslation> GetSimilarTranslations(
            ITranslation sourceTranslation, 
            IList<ITranslation> targetTranslations)
        {
            return targetTranslations
                .Where(translation => translation.Source.Id == sourceTranslation.Source.Id || 
                    translation.Target.Id == sourceTranslation.Target.Id)
                .ToList();
        }
    }
}