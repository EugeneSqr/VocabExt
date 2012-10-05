using System.Collections.Generic;
using VX.Domain.Entities;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure.Factories.Tasks
{
    public abstract class TasksFactoryBase : ITasksFactory
    {
        protected const int DefaultAnswersCount = 3;

        protected readonly IRandomPicker RandomPicker;
        protected readonly ITaskValidator TaskValidator;
        protected readonly ISynonymSelector SynonymSelector;

        protected TasksFactoryBase(IRandomPicker randomPicker, ITaskValidator taskValidator, ISynonymSelector synonymSelector)
        {
            RandomPicker = randomPicker;
            TaskValidator = taskValidator;
            SynonymSelector = synonymSelector;
        }

        public abstract ITask BuildTask(IVocabBank vocabBank);
        
        public virtual ITask BuildTask(IList<IVocabBank> vocabBanks)
        {
            var singleBank = RandomPicker.PickItem(vocabBanks);
            return BuildTask(singleBank);
        }

        public virtual IList<ITask> BuildTasks(IList<IVocabBank> vocabBanks, int tasksCount)
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