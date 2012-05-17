using System.Collections.Generic;
using Autofac;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Factories.Interfaces;
using VX.Service.Repositories.Interfaces;

namespace VX.Service
{
    public class VocabExtService : IVocabExtService
    {
        private readonly ITasksFactory tasksFactory;
        private readonly IVocabBanksRepository vocabBanksRepository;

        // TODO: to config
        private const int DefaultTasksCount = 10;
        
        public VocabExtService()
        {
            tasksFactory = Initializer.Container.Resolve<ITasksFactory>();
            vocabBanksRepository = Initializer.Container.Resolve<IVocabBanksRepository>();
        }

        public ITask GetTask()
        {
            var vocabBanks = vocabBanksRepository.GetVocabBanks();
            return tasksFactory.BuildTask(vocabBanks);
        }

        public IList<ITask> GetTasks()
        {
            var vocabBanks = vocabBanksRepository.GetVocabBanks();
            return tasksFactory.BuildTasks(vocabBanks, DefaultTasksCount);
        }

        public IList<ITask> GetTasks(int[] vocabBanksIds)
        {
            var vocabBanks = vocabBanksRepository.GetVocabBanks(vocabBanksIds);
            return tasksFactory.BuildTasks(vocabBanks, DefaultTasksCount);
        }

        public IList<IVocabBank> GetVocabBanksList()
        {
            return vocabBanksRepository.GetVocabBanksList();
        }
    }
}
