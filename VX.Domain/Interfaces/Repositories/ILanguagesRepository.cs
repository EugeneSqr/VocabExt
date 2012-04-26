using VX.Domain.Interfaces.Entities;

namespace VX.Domain.Interfaces.Repositories
{
    public interface ILanguagesRepository
    {
        ILanguage GetLanguage(int languageId);
    }
}
