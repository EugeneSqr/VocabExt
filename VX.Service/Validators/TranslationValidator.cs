using VX.Domain;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Infrastructure.Factories.ServiceOperationResponses;
using VX.Service.Repositories.Interfaces;
using VX.Service.Validators.Interfaces;

namespace VX.Service.Validators
{
    public class TranslationValidator : ValidatorBase, ITranslationValidator
    {
        private readonly IWordValidator wordValidator;
        private readonly IWordsRepository wordsRepository;

        public TranslationValidator(
            IServiceOperationResponseFactory serviceOperationResponseFactory, 
            IWordValidator wordValidator, 
            IWordsRepository wordsRepository) : base(serviceOperationResponseFactory)
        {
            this.wordValidator = wordValidator;
            this.wordsRepository = wordsRepository;
        }

        // TODO: localize
        public IServiceOperationResponse Validate(ITranslation translation)
        {
            var validationResult = wordValidator.ValidateExist(translation.Source, wordsRepository);
            if (!validationResult.Status)
            {
                validationResult = wordValidator.ValidateExist(translation.Target, wordsRepository);
                return !validationResult.Status
                           ? ServiceOperationResponseFactory.Build(true, ServiceOperationAction.Validate)
                           : BuildFailResponse("Target word does not exist");
            }
            return BuildFailResponse("Source word does not exist");
        }

        private IServiceOperationResponse BuildFailResponse(string errorMessage)
        {
            return ServiceOperationResponseFactory.Build(false, ServiceOperationAction.Validate, errorMessage);
        }
    }
}