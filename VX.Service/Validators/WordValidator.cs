using VX.Domain;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Factories.Interfaces;
using VX.Service.Repositories.Interfaces;
using VX.Service.Validators.Interfaces;

namespace VX.Service.Validators
{
    public class WordValidator : IWordValidator
    {
        private readonly IWordsRepository wordsRepository;
        private readonly ILanguagesRepository languagesRepository;
        private readonly IServiceOperationResponseFactory serviceOperationResponseFactory;

        public WordValidator(
            IWordsRepository wordsRepository, 
            ILanguagesRepository languagesRepository,
            IServiceOperationResponseFactory serviceOperationResponseFactory)
        {
            this.wordsRepository = wordsRepository;
            this.languagesRepository = languagesRepository;
            this.serviceOperationResponseFactory = serviceOperationResponseFactory;
        }

        // TODO: finish implementation and refactoring
        public IServiceOperationResponse ValidateExist(IWord word)
        {
            if (IsEmpty(word))
            {
                return BuildEmptyWordResponse();
            }
            else
            {
                return serviceOperationResponseFactory.Build(true, ServiceOperationAction.Validate);
            }
        }

        public IServiceOperationResponse ValidateSpelling(IWord word)
        {
            if (IsEmpty(word))
            {
                return BuildEmptyWordResponse();
            }
            
            bool isValid = false;
            if (word.Spelling != null)
            {
                isValid = word.Spelling.Trim() != string.Empty;
            }

            return serviceOperationResponseFactory.Build(
                isValid, 
                ServiceOperationAction.Validate);
        }

        public IServiceOperationResponse ValidateLanguage(IWord word)
        {
            if (IsEmpty(word))
            {
                return BuildEmptyWordResponse();
            }
            else
            {
                return serviceOperationResponseFactory.Build(true, ServiceOperationAction.Validate);
            }
        }

        public IServiceOperationResponse Validate(IWord word)
        {
            if (IsEmpty(word))
            {
                return BuildEmptyWordResponse();
            }
            else
            {
                return serviceOperationResponseFactory.Build(true, ServiceOperationAction.Validate);
            }
        }

        private bool IsEmpty(IWord word)
        {
            return word == null;
        }

        private IServiceOperationResponse BuildEmptyWordResponse()
        {
            return serviceOperationResponseFactory.Build(false, ServiceOperationAction.Validate, "The word is null");
        }
    }
}