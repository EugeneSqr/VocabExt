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
    }
}
