using VX.Domain.Interfaces;
using VX.Domain.Interfaces.Entities;

namespace VX.Domain.Surrogates
{
    public class WordSurrogate : IWord
    {
        public int Id { get; set; }

        public string Spelling { get; set; }

        public string Transcription { get; set; }

        public ILanguage Language { get; set; }
    }
}
