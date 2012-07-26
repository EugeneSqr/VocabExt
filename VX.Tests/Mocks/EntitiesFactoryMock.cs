using System.Collections.Generic;
using VX.Domain.DataContracts;
using VX.Domain.DataContracts.Interfaces;
using VX.Model;
using VX.Service.Factories.Interfaces;

namespace VX.Tests.Mocks
{
    public class EntitiesFactoryMock : IEntitiesFactory
    {
        public ILanguage BuildLanguage(Language language)
        {
            return language == null
                       ? null
                       : new LanguageContract();

        }

        public ILanguage BuildLanguage(IDictionary<string, object> language)
        {
            throw new System.NotImplementedException();
        }

        public IWord BuildWord(IDictionary<string, object> word)
        {
            throw new System.NotImplementedException();
        }

        public IWord BuildWord(Word word)
        {
            return word == null
                       ? null
                       : new WordContract
                             {
                                 Id = word.Id,
                                 Spelling = word.Spelling
                             };
        }

        public ITranslation BuildTranslation(IDictionary<string, object> translation)
        {
            throw new System.NotImplementedException();
        }

        public ITranslation BuildTranslation(Translation translation)
        {
            return translation == null
                       ? null
                       : new TranslationContract
                             {
                                 Id = translation.Id,
                                 Source = new WordContract
                                              {
                                                  Id = translation.Source.Id
                                              },
                                 Target = new WordContract
                                              {
                                                  Id = translation.Target.Id
                                              }
                             };
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
    }
}