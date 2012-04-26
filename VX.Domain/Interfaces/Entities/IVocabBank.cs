using System.Collections.Generic;

namespace VX.Domain.Interfaces.Entities
{
    public interface IVocabBank
    {
        int Id { get; set; }

        string Name { get; set; }

        string Description { get; set; }

        IList<ITranslation> Translations { get; set; }

        IList<ITag> Tags { get; set; }
    }
}
