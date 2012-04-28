using VX.Domain.DataContracts.Interfaces;

namespace VX.Domain.DataContracts
{
    public class TranslationContract : ITranslation
    {
        public int Id { get; set; }

        public IWord Source { get; set; }

        public IWord Target { get; set; }
    }
}
