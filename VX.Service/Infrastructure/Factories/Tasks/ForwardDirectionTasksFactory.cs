using System;
using System.Linq;
using VX.Domain.Entities;
using VX.Domain.Entities.Impl;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure.Factories.Tasks
{
    public class ForwardDirectionTasksFactory : TasksFactoryBase
    {
        public ForwardDirectionTasksFactory(IRandomPicker randomPicker, ITaskValidator taskValidator, ISynonymSelector synonymSelector) 
            : base(randomPicker, taskValidator, synonymSelector)
        {
        }

        public override ITask BuildTask(IVocabBank vocabBank)
        {
            var question = RandomPicker.PickItem(vocabBank.Translations);
            var questionBlacklist = SynonymSelector.GetSimilarTranslations(question, vocabBank.Translations);

            var answers = RandomPicker
                .PickItems(vocabBank.Translations, DefaultAnswersCount, questionBlacklist)
                .Select(answer => answer.Target)
                .ToList();

            answers.Insert(RandomPicker.PickInsertIndex(answers), question.Target);

            var result = new TaskContract
                       {
                           Answers = answers,
                           CorrectAnswer = question.Target,
                           Question = question.Source
                       };

            if (TaskValidator.IsValidTask(result))
                return result;
            
            throw new ArgumentException("Task created incorrectly.", "vocabBank");
        }
    }
}