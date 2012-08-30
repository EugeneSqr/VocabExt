using System.Collections.Generic;
using VX.Domain.Entities;

namespace VX.Service.Repositories.Interfaces
{
    public interface ILanguagesRepository
    {
        ILanguage GetLanguage(int languageId);

        IList<ILanguage> GetLanguages();
    }
}
