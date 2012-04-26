using VX.Domain;
using VX.Domain.Interfaces;
using VX.Domain.Interfaces.Entities;
using VX.Domain.Interfaces.Factories;
using VX.Domain.Surrogates;

namespace VX.Service.Factories
{
    public class EntitiesFactory : IEntitiesFactory
    {
        public ILanguage BuildLanguage(Language language)
        {
            return language == null
                       ? null
                       : new LanguageSurrogate
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
                       : new VocabBankSurrogate
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
                       : new TagSurrogate
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
                       : new TranslationSurrogate
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
                       : new WordSurrogate
                             {
                                 Id = word.Id,
                                 Spelling = word.Spelling,
                                 Transcription = word.Transcription,
                                 Language = BuildLanguage(word.Language)
                             };
        }
    }
}