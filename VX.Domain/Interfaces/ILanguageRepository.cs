namespace VX.Domain.Interfaces
{
    public interface ILanguageRepository
    {
        void Add(ILanguage language);

        void Update(ILanguage language);

        void Remove(ILanguage language);

        ILanguage GetById(int languageId);
    }
}
