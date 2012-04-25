using VX.Domain;
using VX.Domain.Interfaces;
using VX.Domain.Surrogates;

namespace VX.Tests.Mocks
{
    internal class EntitiesFactoryMock : IEntitiesFactory
    {
        public ILanguage BuildLanguage(Language language)
        {
            return language == null
                       ? null
                       : new LanguageSurrogate();

        }

        public IVocabBank BuildVocabBank(VocabBank vocabBank)
        {
            return vocabBank == null
                       ? null
                       : new VocabBankSurrogate();
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