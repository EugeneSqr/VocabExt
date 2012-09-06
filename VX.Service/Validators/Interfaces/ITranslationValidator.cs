using VX.Domain.Entities;
using VX.Domain.Responses;

namespace VX.Service.Validators.Interfaces
{
    public interface ITranslationValidator
    {
        IOperationResponse Validate(ITranslation translation);
    }
}