using VX.Domain;
using VX.Domain.DataContracts;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Factories.Interfaces;

namespace VX.Tests.Mocks
{
    internal class EntitiesFactoryMock : IEntitiesFactory
    {
        public ILanguage BuildLanguage(Language language)
        {
            return language == null
                       ? null
                       : new LanguageContract();

        }

        public IVocabBank BuildVocabBank(VocabBank vocabBank)
        {
            return vocabBank == null
                       ? null
                       : new VocabBankContract();
        }

        public ITag BuildTag(Tag tag)
        {
            throw new System.NotImplementedException();
        }

        public ITranslation BuildTranslation(Translation translation)
        {
            throw new System.NotImplementedException();
        }

        public IWord BuildWord(Word word)
        {
            throw new System.NotImplementedException();
        }
    }
}