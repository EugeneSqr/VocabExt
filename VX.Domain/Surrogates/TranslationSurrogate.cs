using VX.Domain.Interfaces;
using VX.Domain.Interfaces.Entities;

namespace VX.Domain.Surrogates
{
    public class TranslationSurrogate : ITranslation
    {
        public int Id { get; set; }

        public IWord Source { get; set; }

        public IWord Target { get; set; }
    }
}
