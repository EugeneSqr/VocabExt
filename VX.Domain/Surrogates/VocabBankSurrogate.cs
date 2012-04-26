using System.Collections.Generic;
using VX.Domain.Interfaces;
using VX.Domain.Interfaces.Entities;

namespace VX.Domain.Surrogates
{
    public class VocabBankSurrogate : IVocabBank
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IList<ITranslation> Translations { get; set; }

        public IList<ITag> Tags { get; set; }
    }
}
