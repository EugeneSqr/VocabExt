using System;
using System.Collections.Generic;
using System.Linq;
using VX.Domain.Entities;
using VX.Domain.Entities.Impl;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure.Factories.Tasks
{
    public class TasksFactory : ITasksFactory
    {
        private const int DefaultAnswersCount = 3;
        
        private readonly IRandomPicker randomPicker;
        private readonly ITaskValidator taskValidator;
        private readonly ISynonymSelector synonymSelector;
        
        public TasksFactory(IRandomPicker randomPicker, ITaskValidator taskValidator, ISynonymSelector synonymSelector)
        {
            this.randomPicker = randomPicker;
            this.taskValidator = taskValidator;
            this.synonymSelector = synonymSelector;
        }

        public ITask BuildTask(IVocabBank vocabBank)
        {
            var question = randomPicker.PickItem(vocabBank.Translations);
            var questionBlacklist = synonymSelector.GetSimilarTranslations(question, vocabBank.Translations);

            var answers = randomPicker
                .PickItems(vocabBank.Translations, DefaultAnswersCount, questionBlacklist)
                .Select(answer => answer.Target)
                .ToList();

            answers.Insert(randomPicker.PickInsertIndex(answers), question.Target);

            var result = new TaskContract
                       {
                           Answers = answers,
                           CorrectAnswer = question.Target,
                           Question = question.Source
                       };

            if (taskValidator.IsValidTask(result))
                return result;
            
            // TODO: localize
            throw new ArgumentException("Task created incorrectly.", "vocabBank");
        }

        public ITask BuildTask(IList<IVocabBank> vocabBanks)
        {
            var singleBank = randomPicker.PickItem(vocabBanks);
            return BuildTask(singleBank);
        }

        public IList<ITask> BuildTasks(IList<IVocabBank> vocabBanks, int tasksCount)
        {
            var result = new List<ITask>();
            if (tasksCount > 0)
            {
                for (int i = 0; i < tasksCount; i++)
                {
                    result.Add(BuildTask(vocabBanks));
                }
            }

            return result;
        }
    }
}