using System.Collections.Generic;
using System.IO;
using System.Linq;
using VX.Domain.Entities;
using VX.Domain.Responses;
using VX.Domain.Surrogates;
using VX.Service.Infrastructure.Factories.Entities;
using VX.Service.Infrastructure.Factories.Responses;
using VX.Service.Infrastructure.Factories.Surrogates;
using VX.Service.Infrastructure.Factories.Tasks;
using VX.Service.Infrastructure.Interfaces;
using VX.Service.Repositories.Interfaces;
using VX.Service.Validators.Interfaces;

namespace VX.Service
{
    public class VocabExtService : IVocabExtService
    {
        private readonly ITasksFactory tasksFactory;
        private readonly IVocabBanksRepository vocabBanksRepository;
        private readonly ITranslationsRepository translationsRepository;
        private readonly IWordsRepository wordsRepository;
        private readonly IServiceSettings serviceSettings;
        private readonly IAbstractEntitiesFactory entitiesFactory;
        private readonly ILanguagesRepository languagesRepository;
        private readonly IWordValidator wordValidator;
        private readonly IResponsesFactory responsesFactory;
        private readonly ISurrogatesFactory surrogatesFactory;

        public VocabExtService(
            ITasksFactory tasksFactory, 
            IVocabBanksRepository vocabBanksRepository, 
            ITranslationsRepository translationsRepository, 
            IWordsRepository wordsRepository, 
            IServiceSettings serviceSettings, 
            IAbstractEntitiesFactory entitiesFactory, 
            ILanguagesRepository languagesRepository, 
            IWordValidator wordValidator, 
            IResponsesFactory responsesFactory, 
            ISurrogatesFactory surrogatesFactory)
        {
            this.tasksFactory = tasksFactory;
            this.vocabBanksRepository = vocabBanksRepository;
            this.translationsRepository = translationsRepository;
            this.wordsRepository = wordsRepository;
            this.serviceSettings = serviceSettings;
            this.entitiesFactory = entitiesFactory;
            this.languagesRepository = languagesRepository;
            this.wordValidator = wordValidator;
            this.responsesFactory = responsesFactory;
            this.surrogatesFactory = surrogatesFactory;
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

        public IOperationResponse DeleteVocabularyBank(string vocabBankId)
        {
            // TODO: parse safely
            return responsesFactory.Create(
                vocabBanksRepository.Delete(int.Parse(vocabBankId)), 
                ServiceOperationAction.Delete);
        }

        public IOperationResponse SaveTranslation(Stream data)
        {
            var parsedPair = surrogatesFactory.CreateBankTranslationPair(data);
            IManyToManyRelationship translation;
            ServiceOperationAction action;
            translationsRepository.SaveTranslation(parsedPair.Translation, out translation, out action);
            return translation != null
                       ? responsesFactory.Create(
                           vocabBanksRepository.AttachTranslation(parsedPair.VocabBankId, translation.Id),
                           ServiceOperationAction.Attach)
                       : responsesFactory.Create(false, ServiceOperationAction.Create);
        }

        public IOperationResponse DetachTranslation(Stream data)
        {
            var parsedPair = surrogatesFactory.CreateParentChildIdPair(data);
            return responsesFactory.Create(
                vocabBanksRepository.DetachTranslation(parsedPair.ParentId, parsedPair.ChildId),
                ServiceOperationAction.Detach);
        }

        public IOperationResponse UpdateBankHeaders(Stream data)
        {
            /*var aaa = entitiesFactory.Create<IVocabBankSummary, Stream>(data);
            return vocabBanksRepository.UpdateHeaders(
                inputDataConverter.ParseBankSummary(data));*/
            return null;
        }

        public IOperationResponse SaveWord(Stream data)
        {
            return responsesFactory.Create(
                wordsRepository.SaveWord(entitiesFactory.Create<IWord, Stream>(data)),
                ServiceOperationAction.Create);

        }

        public IOperationResponse ValidateWord(Stream data)
        {
            return wordValidator.Validate(entitiesFactory.Create<IWord, Stream>(data), wordsRepository);
        }
    }
}
