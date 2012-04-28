using VX.Domain;
using VX.Domain.DataContracts;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Factories.Interfaces;

namespace VX.Service.Factories
{
    public class EntitiesFactory : IEntitiesFactory
    {
        public ILanguage BuildLanguage(Language language)
        {
            return language == null
                       ? null
                       : new LanguageContract
                             {
                                 Id = language.Id,
                                 Name = language.Name,
                                 Abbreviation = language.Abbreviation
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
                             };
        }

        public ITag BuildTag(Tag tag)
        {
            return tag == null
                       ? null
                       : new TagContract
                             {
                                 Id = tag.Id,
                                 Name = tag.Name,
                                 Description = tag.Description
                             };
        }

        public ITranslation BuildTranslation(Translation translation)
        {
            return translation == null
                       ? null
                       : new TranslationContract
                             {
                                 Id = translation.Id,
                                 Source = BuildWord(translation.Source),
                                 Target = BuildWord(translation.Target)
                             };
        }

        public IWord BuildWord(Word word)
        {
            return word == null
                       ? null
                       : new WordContract
                             {
                                 Id = word.Id,
                                 Spelling = word.Spelling,
                                 Transcription = word.Transcription,
                                 Language = BuildLanguage(word.Language)
                             };
        }
    }
}