using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Autofac;
using VX.Domain.DataContracts;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Factories.Interfaces;
using VX.Service.Infrastructure.Interfaces;
using VX.Service.Repositories.Interfaces;

namespace VX.Service
{
    public class VocabExtService : IVocabExtService
    {
        private readonly ITasksFactory tasksFactory;
        private readonly IVocabBanksRepository vocabBanksRepository;
        private readonly ITranslationsRepository translationsRepository;
        private readonly IWordsRepository wordsRepository;
        private readonly IServiceSettings serviceSettings;

        public VocabExtService()
        {
            tasksFactory = Initializer.Container.Resolve<ITasksFactory>();
            vocabBanksRepository = Initializer.Container.Resolve<IVocabBanksRepository>();
            translationsRepository = Initializer.Container.Resolve<ITranslationsRepository>();
            serviceSettings = Initializer.Container.Resolve<IServiceSettings>();
            wordsRepository = Initializer.Container.Resolve<IWordsRepository>();
        }

        public ITask GetTask()
        {
            var vocabBanks = vocabBanksRepository.GetVocabBanks();
            return tasksFactory.BuildTask(vocabBanks);
        }

        public IList<ITask> GetTasks()
        {
            var vocabBanks = vocabBanksRepository.GetVocabBanks();
            return tasksFactory.BuildTasks(vocabBanks, serviceSettings.DefaultTasksCount);
        }

        public IList<ITask> GetTasks(int[] vocabBanksIds)
        {
            var vocabBanks = vocabBanksRepository.GetVocabBanks(vocabBanksIds);
            return tasksFactory.BuildTasks(vocabBanks, serviceSettings.DefaultTasksCount);
        }

        public IList<IVocabBank> GetVocabBanksList()
        {
            return vocabBanksRepository.GetVocabBanksList();
        }

        public IList<ITranslation> GetTranslations(string vocabBankId)
        {
            return translationsRepository.GetTranslations(vocabBankId);
        }

        public IList<IWord> GetWords(string searchString)
        {
            return wordsRepository.GetWords(searchString);
        }

        public IServiceOperationResponse UpdateTranslation(Stream data)
        {
            // TODO: to special factory
            var serializer = new DataContractJsonSerializer(typeof (TranslationContract));
            return translationsRepository.UpdateTranslation((ITranslation)serializer.ReadObject(data));
        }
    }
}
