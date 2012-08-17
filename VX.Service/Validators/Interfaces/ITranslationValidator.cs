using VX.Domain;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Service.Validators.Interfaces
{
    public interface ITranslationValidator
    {
        IServiceOperationResponse Validate(ITranslation translation);
    }
}