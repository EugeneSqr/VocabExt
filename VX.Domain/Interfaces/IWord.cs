namespace VX.Domain.Interfaces
{
    public interface IWord
    {
        int Id { get; set; }

        string Spelling { get; set; }

        string Transcription { get; set; }

        ILanguage Language { get; set; }
    }
}
