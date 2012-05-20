using System.Collections.Generic;
using System.Linq;
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
                       : new VocabBankContract
                             {
                                 Id = vocabBank.Id,
                                 Name = vocabBank.Name,
                                 Description = vocabBank.Description,
                                 Tags =
                                     vocabBank.VocabBanksTags.Count == 0
                                         ? new List<ITag>()
                                         : new List<ITag> {new TagContract()},
                                 Translations =
                                     vocabBank.VocabBanksTranslations.Count == 0
                                         ? new List<ITranslation>()
                                         : new List<ITranslation> {new TranslationContract()}
                             };
        }

        public ITag BuildTag(Tag tag)
        {
            throw new System.NotImplementedException();
        }

        public ITranslation BuildTranslation(Translation translation)
        {
            return new TranslationContract();
        }

        public IWord BuildWord(Word word)
        {
            throw new System.NotImplementedException();
        }
    }
}