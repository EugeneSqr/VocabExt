using System.Collections.Generic;
using System.Linq;

using VX.Domain;
using VX.Domain.Interfaces;
using VX.Domain.Interfaces.Entities;
using VX.Domain.Interfaces.Factories;
using VX.Service.Interfaces;

namespace VX.Service.Factories
{
    public class TasksFactory : ITasksFactory
    {
        private const int DefaultAnswersCount = 4;
        
        private readonly IRandomPicker randomPicker;
        
        public TasksFactory(IRandomPicker randomPicker)
        {
            this.randomPicker = randomPicker;
        }

        public ITask BuildTask(IVocabBank vocabBank)
        {
            var translation = randomPicker.PickItem(vocabBank.Translations);
            var answers = randomPicker
                .PickItems(vocabBank.Translations, DefaultAnswersCount, new List<ITranslation> { translation })
                .Select(answer => answer.Target)
                .ToList();

            return new Task
                       {
                           Answers = answers,
                           CorrectAnswer = translation.Target,
                           Question = translation.Source
                       };
        }

        public ITask BuildTask(IList<IVocabBank> vocabBanks)
        {
            var singleBank = randomPicker.PickItem(vocabBanks);
            return BuildTask(singleBank);
        }
    }
}