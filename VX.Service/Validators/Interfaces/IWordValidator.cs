using VX.Domain;
using VX.Domain.Entities;
using VX.Domain.Responses;
using VX.Domain.Surrogates;
using VX.Service.Repositories.Interfaces;

namespace VX.Service.Validators.Interfaces
{
    public interface IWordValidator
    {
        IOperationResponse ValidateExist(IWord word, IWordsRepository wordsRepository);

        IOperationResponse ValidateSpelling(IWord word);

        IOperationResponse ValidateLanguage(IWord word);

        IOperationResponse Validate(IWord word, IWordsRepository wordsRepository);
    }
}