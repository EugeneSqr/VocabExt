using System.Collections.Generic;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Service.Infrastructure.Factories.Tasks
{
    public interface ITasksFactory
    {
        ITask BuildTask(IVocabBank vocabBank);

        ITask BuildTask(IList<IVocabBank> vocabBanks);

        IList<ITask> BuildTasks(IList<IVocabBank> vocabBanks, int tasksCount);
    }
}
