using VX.Domain;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Repositories.Interfaces;
using VX.Service.Validators.Interfaces;

namespace VX.Service.Validators
{
    public class TranslationValidator : ITranslationValidator
    {
        private readonly IWordValidator wordValidator;
        private readonly IWordsRepository wordsRepository;

        public TranslationValidator(IWordValidator wordValidator, IWordsRepository wordsRepository)
        {
            this.wordValidator = wordValidator;
            this.wordsRepository = wordsRepository;
        }

        public IServiceOperationResponse Validate(ITranslation translation)
        {
            var sourceValidationResult = wordValidator.ValidateExist(translation.Source, wordsRepository);
            return !sourceValidationResult.Status 
                ? sourceValidationResult 
                : wordValidator.ValidateExist(translation.Target, wordsRepository);
        }
    }
}