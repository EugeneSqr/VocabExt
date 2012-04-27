using VX.Domain;
using VX.Domain.Interfaces;
using VX.Domain.Interfaces.Entities;
using VX.Domain.Interfaces.Factories;
using VX.Domain.Surrogates;

namespace VX.Service.Factories
{
    public class ContractItemsFactory : IContractItemsFactory
    {
        public Task BuildTask(ITask task)
        {
            return new Task
                       {
                           Answers = task.Answers,
                           CorrectAnswer = task.CorrectAnswer,
                           Question = task.Question
                       };
        }

        public LanguageSurrogate BuildLanguage(ILanguage language)
        {
            return new LanguageSurrogate
                       {
                           Id = language.Id,
                           Name = language.Name,
                           Abbreviation = language.Abbreviation
                       };
        }

        public WordSurrogate BuildWord(IWord word)
        {
            return new WordSurrogate
                       {
                           Id = word.Id,
                           Spelling = word.Spelling,
                           Transcription = word.Transcription,
                           Language = BuildLanguage(word.Language)
                       };
        }
    }
}