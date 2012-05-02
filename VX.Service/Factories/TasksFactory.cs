using System;
using System.Collections.Generic;
using System.Linq;

using VX.Domain.DataContracts;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Factories.Interfaces;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Factories
{
    public class TasksFactory : ITasksFactory
    {
        private const int DefaultAnswersCount = 3;
        
        private readonly IRandomPicker randomPicker;
        private readonly ITaskValidator taskValidator;
        
        public TasksFactory(IRandomPicker randomPicker, ITaskValidator taskValidator)
        {
            this.randomPicker = randomPicker;
            this.taskValidator = taskValidator;
        }

        public ITask BuildTask(IVocabBank vocabBank)
        {
            var question = randomPicker.PickItem(vocabBank.Translations);
            var answers = randomPicker
                .PickItems(vocabBank.Translations, DefaultAnswersCount, new List<ITranslation> { question })
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
    }
}