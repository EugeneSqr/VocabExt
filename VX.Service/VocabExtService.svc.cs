using System.Collections.Generic;
using System.IO;
using System.Linq;
using VX.Domain.Entities;
using VX.Domain.Surrogates;
using VX.Service.Infrastructure.Factories;
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
        private readonly IAbstractFactory entitiesFactory;
        private readonly ILanguagesRepository languagesRepository;
        private readonly IWordValidator wordValidator;

        public VocabExtService(ITasksFactory tasksFactory, IVocabBanksRepository vocabBanksRepository, ITranslationsRepository translationsRepository, IWordsRepository wordsRepository, IServiceSettings serviceSettings, IAbstractFactory entitiesFactory, ILanguagesRepository languagesRepository, IWordValidator wordValidator)
        {
            this.tasksFactory = tasksFactory;
            this.vocabBanksRepository = vocabBanksRepository;
            this.translationsRepository = translationsRepository;
            this.wordsRepository = wordsRepository;
            this.serviceSettings = serviceSettings;
            this.entitiesFactory = entitiesFactory;
            this.languagesRepository = languagesRepository;
            this.wordValidator = wordValidator;
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
            // TODO: parse safely
            return translationsRepository.GetTranslations(int.Parse(vocabBankId));
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
            // TODO: parse safely
            return vocabBanksRepository.Delete(int.Parse(vocabBankId));
        }

        public IServiceOperationResponse SaveTranslation(Stream data)
        {
            // TODO: surrogate factory
            var parsedPair = entitiesFactory.Create<IBankTranslationPair, Stream>(data);
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
            // TODO: surrogate factory
            var parsedPair = entitiesFactory.Create<IParentChildIdPair, Stream>(data);
            return vocabBanksRepository.DetachTranslation(parsedPair.ParentId, parsedPair.ChildId);
        }

        public IServiceOperationResponse UpdateBankHeaders(Stream data)
        {
            /*var aaa = entitiesFactory.Create<IVocabBankSummary, Stream>(data);
            return vocabBanksRepository.UpdateHeaders(
                inputDataConverter.ParseBankSummary(data));*/
            return null;
        }

        public IServiceOperationResponse SaveWord(Stream data)
        {
            return wordsRepository.SaveWord(entitiesFactory.Create<IWord, Stream>(data));
        }

        public IServiceOperationResponse ValidateWord(Stream data)
        {
            return wordValidator.Validate(entitiesFactory.Create<IWord, Stream>(data), wordsRepository);
        }
    }
}
