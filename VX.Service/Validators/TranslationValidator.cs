using VX.Domain;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Validators.Interfaces;

namespace VX.Service.Validators
{
    public class TranslationValidator : ITranslationValidator
    {
        private readonly IWordValidator wordValidator;

        public TranslationValidator(IWordValidator wordValidator)
        {
            this.wordValidator = wordValidator;
        }

        public IServiceOperationResponse Validate(ITranslation translation)
        {
            var sourceValidationResult = wordValidator.ValidateExist(translation.Source);
            return !sourceValidationResult.Status 
                ? sourceValidationResult 
                : wordValidator.ValidateExist(translation.Target);
        }
    }
}