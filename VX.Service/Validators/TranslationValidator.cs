using VX.Domain.Entities;
using VX.Domain.Surrogates;
using VX.Service.Infrastructure.Factories;
using VX.Service.Repositories.Interfaces;
using VX.Service.Validators.Interfaces;

namespace VX.Service.Validators
{
    public class TranslationValidator : ValidatorBase, ITranslationValidator
    {
        private readonly IWordValidator wordValidator;
        private readonly IWordsRepository wordsRepository;

        public TranslationValidator(
            IAbstractFactory factory, 
            IWordValidator wordValidator, 
            IWordsRepository wordsRepository) : base(factory)
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
                           ? Factory.Create<IServiceOperationResponse>(true, ServiceOperationAction.Validate)
                           : BuildFailResponse("Target word does not exist");
            }
            return BuildFailResponse("Source word does not exist");
        }

        private IServiceOperationResponse BuildFailResponse(string errorMessage)
        {
            return Factory.Create<IServiceOperationResponse>(false, ServiceOperationAction.Validate, errorMessage);
        }
    }
}