using System.Collections.Generic;
using System.ServiceModel.Web;
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
        private readonly ITranslationsRepository translationsRepository;

        // TODO: to config
        private const int DefaultTasksCount = 10;
        
        public VocabExtService()
        {
            tasksFactory = Initializer.Container.Resolve<ITasksFactory>();
            vocabBanksRepository = Initializer.Container.Resolve<IVocabBanksRepository>();
            translationsRepository = Initializer.Container.Resolve<ITranslationsRepository>();
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
            AllowCrossDomainMessages();
            return vocabBanksRepository.GetVocabBanksList();
        }

        public IList<ITranslation> GetTranslations(string vocabBankId)
        {
            AllowCrossDomainMessages();
            return translationsRepository.GetTranslations(vocabBankId);
        }

        private static void AllowCrossDomainMessages()
        {
            var operationContext = WebOperationContext.Current;
            if (operationContext != null)
            {
                // TODO: change * to trust host
                operationContext.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
                operationContext.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods", "GET, POST");
                operationContext.OutgoingResponse.Headers.Add("Access-Control-Allow-Credentials", "false");
            }
        }
    }
}
