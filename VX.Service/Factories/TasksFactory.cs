using System.Collections.Generic;
using VX.Domain.Interfaces;
using VX.Domain.Interfaces.Entities;
using VX.Domain.Interfaces.Factories;

namespace VX.Service.Factories
{
    public class TasksFactory : ITasksFactory
    {
        public ITask BuildTask(IVocabBank vocabBank)
        {
            throw new System.NotImplementedException();
        }

        public ITask BuilTask(IList<IVocabBank> vocabBanks)
        {
            throw new System.NotImplementedException();
        }
    }
}