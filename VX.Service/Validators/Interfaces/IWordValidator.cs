using VX.Domain;
using VX.Domain.Entities;
using VX.Domain.Surrogates;
using VX.Service.Repositories.Interfaces;

namespace VX.Service.Validators.Interfaces
{
    public interface IWordValidator
    {
        IServiceOperationResponse ValidateExist(IWord word, IWordsRepository wordsRepository);

        IServiceOperationResponse ValidateSpelling(IWord word);

        IServiceOperationResponse ValidateLanguage(IWord word);

        IServiceOperationResponse Validate(IWord word, IWordsRepository wordsRepository);
    }
}