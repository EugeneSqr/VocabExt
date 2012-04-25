using VX.Domain;
using VX.Domain.Interfaces;
using VX.Domain.Surrogates;

namespace VX.Service
{
    public class EntitiesFactory : IEntitiesFactory
    {
        public ILanguage BuildLanguage(Language language)
        {
            if (language == null)
            {
                return null;
            }

            return new LanguageSurrogate
                       {
                           Id = language.Id,
                           Name = language.Name,
                           Abbreviation = language.Abbreviation
                       };
        }
    }
}