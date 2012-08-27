using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autofac;
using VX.Domain;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Infrastructure.Factories.Tasks;
using VX.Service.Infrastructure.Interfaces;
using VX.Service.Repositories.Interfaces;
using VX.Service.Validators.Interfaces;

namespace VX.Service
{
    // TODO: create dispatcher to move logic to separate classes
    // TODO: or use this class as dispatcher and add unit tests
    public class VocabExtService : IVocabExtService
    {
        private readonly ITasksFactory tasksFactory;
        private readonly IVocabBanksRepository vocabBanksRepository;
        private readonly ITranslationsRepository translationsRepository;
        private readonly IWordsRepository wordsRepository;
        private readonly IServiceSettings serviceSettings;
        private readonly IInputDataConverter inputDataConverter;
        private readonly ILanguagesRepository languagesRepository;
        private readonly IWordValidator wordValidator;

        public VocabExtService()
        {
            tasksFactory = Initializer.Container.Resolve<ITasksFactory>();
            vocabBanksRepository = Initializer.Container.Resolve<IVocabBanksRepository>();
            translationsRepository = Initializer.Container.Resolve<ITranslationsRepository>();
            serviceSettings = Initializer.Container.Resolve<IServiceSettings>();
            wordsRepository = Initializer.Container.Resolve<IWordsRepository>();
            inputDataConverter = Initializer.Container.Resolve<IInputDataConverter>();
            languagesRepository = Initializer.Container.Resolve<ILanguagesRepository>();
            wordValidator = Initializer.Container.Resolve<IWordValidator>();
        }

        public ITask GetTask()
        {
            var vocabBanks = vocabBanksRepository.Get();
            return tasksFactory.BuildTask(vocabBanks);
        }

        public IList<ITask> GetTasks()
        {
            IList<ITask> result = new List<ITask>();
            var banks = vocabBanksRepository.GetWithTranslationsOnly();
            if (banks.Any())
            {
                result = tasksFactory.BuildTasks(
                    vocabBanksRepository.GetWithTranslationsOnly(),
                    serviceSettings.DefaultTasksCount);
            }

            return result;
        }

        public IList<ITask> GetTasks(int[] vocabBanksIds)
        {
            var banks = vocabBanksRepository.GetWithTranslationsOnly(vocabBanksIds);
            return banks.Any() 
                ? tasksFactory.BuildTasks(banks, serviceSettings.DefaultTasksCount) 
                : GetTasks();
        }

        public IList<IVocabBank> GetVocabBanksList()
        {
            return vocabBanksRepository.GetListWithoutTranslations();
        }

        public IList<ITranslation> GetTranslations(string vocabBankId)
        {
            return translationsRepository.GetTranslations(inputDataConverter.Convert(vocabBankId));
        }

        public IList<IWord> GetWords(string searchString)
        {
            return wordsRepository.GetWords(searchString);
        }

        public IVocabBank CreateNewVocabularyBank()
        {
            return vocabBanksRepository.Create();
        }

        public IList<ILanguage> GetLanguages()
        {
            return languagesRepository.GetLanguages();
        }

        public IServiceOperationResponse DeleteVocabularyBank(string vocabBankId)
        {
            return vocabBanksRepository.Delete(inputDataConverter.Convert(vocabBankId));
        }

        public IServiceOperationResponse SaveTranslation(Stream data)
        {
            var parsedPair = inputDataConverter.ParseBankTranslationPair(data);
            IManyToManyRelationship translation;
            IServiceOperationResponse result = translationsRepository.SaveTranslation(parsedPair.Translation, out translation);
            if (translation != null)
            {
                result = vocabBanksRepository.AttachTranslation(parsedPair.VocabBankId, translation.Id);
            }

            return result;
        }

        public IServiceOperationResponse DetachTranslation(Stream data)
        {
            var parsedPair = inputDataConverter.ParsePair(data);
            return vocabBanksRepository.DetachTranslation(parsedPair.ParentId, parsedPair.ChildId);
        }

        public IServiceOperationResponse UpdateBankHeaders(Stream data)
        {
            return vocabBanksRepository.UpdateHeaders(
                inputDataConverter.ParseBankHeaders(data));
        }

        public IServiceOperationResponse SaveWord(Stream data)
        {
            return wordsRepository.SaveWord(inputDataConverter.ParseWord(data));
        }

        public IServiceOperationResponse ValidateWord(Stream data)
        {
            return wordValidator.Validate(inputDataConverter.ParseWord(data), wordsRepository);
        }
    }
}
