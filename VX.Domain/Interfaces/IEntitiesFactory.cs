namespace VX.Domain.Interfaces
{
    public interface IEntitiesFactory
    {
        ILanguage BuildLanguage(Language language);

        IVocabBank BuildVocabBank(VocabBank vocabBank);

        ITag BuildTag(Tag tag);

        ITranslation BuildTranslation(Translation translation);

        IWord BuildWord(Word word);
    }
}
