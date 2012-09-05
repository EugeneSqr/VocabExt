using VX.Domain.Entities;
using VX.Domain.Responses;
using VX.Domain.Surrogates;
using VX.Service.Infrastructure.Factories.Responses;
using VX.Service.Repositories.Interfaces;
using VX.Service.Validators.Interfaces;

namespace VX.Service.Validators
{
    [RegisterService]
    public class TranslationValidator : ValidatorBase, ITranslationValidator
    {
        private readonly IWordValidator wordValidator;
        private readonly IWordsRepository wordsRepository;

        public TranslationValidator(
            IResponsesFactory responsesFactory, 
            IWordValidator wordValidator, 
            IWordsRepository wordsRepository) : base(responsesFactory)
        {
            this.wordValidator = wordValidator;
            this.wordsRepository = wordsRepository;
        }

        // TODO: localize
        public IOperationResponse Validate(ITranslation translation)
        {
            var validationResult = wordValidator.ValidateExist(translation.Source, wordsRepository);
            if (!validationResult.Status)
            {
                validationResult = wordValidator.ValidateExist(translation.Target, wordsRepository);
                return !validationResult.Status
                           ? ResponsesFactory.Create(true, ServiceOperationAction.Validate)
                           : BuildFailResponse("Target word does not exist");
            }
            return BuildFailResponse("Source word does not exist");
        }

        private IOperationResponse BuildFailResponse(string errorMessage)
        {
            return ResponsesFactory.Create(false, ServiceOperationAction.Validate, errorMessage);
        }
    }
}