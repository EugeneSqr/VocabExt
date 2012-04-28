using System.Collections.Generic;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Domain.DataContracts
{
    public class VocabBankContract : IVocabBank
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IList<ITranslation> Translations { get; set; }

        public IList<ITag> Tags { get; set; }
    }
}
