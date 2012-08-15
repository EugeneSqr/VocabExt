using VX.Domain;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Service.Validators.Interfaces
{
    public interface IWordValidator
    {
        IServiceOperationResponse ValidateExist(IWord word);

        IServiceOperationResponse ValidateSpelling(IWord word);

        IServiceOperationResponse ValidateLanguage(IWord word);

        IServiceOperationResponse Validate(IWord word);
    }
}