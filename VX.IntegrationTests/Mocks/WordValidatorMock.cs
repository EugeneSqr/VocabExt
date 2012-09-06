using VX.Domain.Entities;
using VX.Domain.Responses;
using VX.Service.Repositories.Interfaces;
using VX.Service.Validators.Interfaces;

namespace VX.IntegrationTests.Mocks
{
    public class WordValidatorMock : IWordValidator
    {
        public IOperationResponse ValidateExist(IWord word, IWordsRepository wordsRepository)
        {
            throw new System.NotImplementedException();
        }

        public IOperationResponse ValidateSpelling(IWord word)
        {
            throw new System.NotImplementedException();
        }

        public IOperationResponse ValidateLanguage(IWord word)
        {
            throw new System.NotImplementedException();
        }

        public IOperationResponse Validate(IWord word, IWordsRepository wordsRepository)
        {
            throw new System.NotImplementedException();
        }
    }
}
