using System.Collections.Generic;
using VX.Domain.Interfaces.Entities;

namespace VX.Domain.Interfaces.Factories
{
    public interface ITasksFactory
    {
        ITask BuildTask(IVocabBank vocabBank);

        ITask BuildTask(IList<IVocabBank> vocabBanks);
    }
}
