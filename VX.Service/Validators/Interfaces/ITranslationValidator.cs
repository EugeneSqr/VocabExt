using VX.Domain;
using VX.Domain.Entities;
using VX.Domain.Surrogates;

namespace VX.Service.Validators.Interfaces
{
    public interface ITranslationValidator
    {
        IServiceOperationResponse Validate(ITranslation translation);
    }
}