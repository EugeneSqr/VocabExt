using System.Collections.Generic;
using VX.Domain.Interfaces;

namespace VX.Domain.Surrogates
{
    public class VocabBankSurrogate : IVocabBank
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IList<ITranslation> Translations { get; set; }

        public IList<ITag> Tags { get; set; }
    }
}
