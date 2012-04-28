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

            return new TaskContract
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