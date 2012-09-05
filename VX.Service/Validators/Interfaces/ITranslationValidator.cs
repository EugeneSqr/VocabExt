using VX.Domain;
using VX.Domain.Entities;
using VX.Domain.Responses;
using VX.Domain.Surrogates;

namespace VX.Service.Validators.Interfaces
{
    public interface ITranslationValidator
    {
        IOperationResponse Validate(ITranslation translation);
    }
}