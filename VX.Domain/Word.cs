using VX.Domain.Interfaces;

namespace VX.Domain
{
    public class Word : IWord
    {
        public int Id { get; set; }

        public string Spelling { get; set; }

        public string Transcription { get; set; }

        public ILanguage Language { get; set; }
    }
}
