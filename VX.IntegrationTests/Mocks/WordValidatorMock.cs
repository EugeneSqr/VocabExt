using VX.Domain;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Repositories.Interfaces;
using VX.Service.Validators.Interfaces;

namespace VX.IntegrationTests.Mocks
{
    public class WordValidatorMock : IWordValidator
    {
        public IServiceOperationResponse ValidateExist(IWord word, IWordsRepository wordsRepository)
        {
            throw new System.NotImplementedException();
        }

        public IServiceOperationResponse ValidateSpelling(IWord word)
        {
            throw new System.NotImplementedException();
        }

        public IServiceOperationResponse ValidateLanguage(IWord word)
        {
            throw new System.NotImplementedException();
        }

        public IServiceOperationResponse Validate(IWord word, IWordsRepository wordsRepository)
        {
            throw new System.NotImplementedException();
        }
    }
}
