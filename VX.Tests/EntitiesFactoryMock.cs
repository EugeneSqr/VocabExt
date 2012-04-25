using VX.Domain;
using VX.Domain.Interfaces;
using VX.Domain.Surrogates;

namespace VX.Tests
{
    internal class EntitiesFactoryMock : IEntitiesFactory
    {
        public ILanguage BuildLanguage(Language language)
        {
            return language == null
                       ? null
                       : new LanguageSurrogate
                             {
                                 Id = 1
                             };
        }
    }
}