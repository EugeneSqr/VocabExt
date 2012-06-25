using System.ComponentModel.DataAnnotations;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Service.CompositeValidators.Interfaces
{
    public interface ITranslationValidator
    {
        ValidationResult Validate(ITranslation translation);
    }
}