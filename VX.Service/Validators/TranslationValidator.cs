using System.ComponentModel.DataAnnotations;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Repositories.Interfaces;
using VX.Service.Validators.Interfaces;

namespace VX.Service.Validators
{
    public class TranslationValidator : ITranslationValidator
    {
        private readonly IWordsRepository wordsRepository;
        
        public TranslationValidator(IWordsRepository wordsRepository)
        {
            this.wordsRepository = wordsRepository;
        }

        public ValidationResult Validate(ITranslation translation)
        {
            var sourceValidationResult = ValidateWord(translation.Source);
            if (sourceValidationResult != ValidationResult.Success)
                return sourceValidationResult;

            var targetValidationResult = ValidateWord(translation.Target);
            return targetValidationResult != ValidationResult.Success 
                       ? targetValidationResult 
                       : ValidationResult.Success;
        }

        private ValidationResult ValidateWord(IWord word)
        {
            return wordsRepository.GetWord(word.Id) != null
                       ? ValidationResult.Success
                       : new ValidationResult(
                             string.Format("The word with id: {0} Doesn't exist.", word.Id));
        }
    }
}