using System.IO;

namespace VX.Service.Infrastructure.Interfaces
{
    public interface IInputDataConverter
    {
        int EmptyId { get; }

        int Convert(string id);

        IParentChildIdPair ParsePair(Stream data);

        IBankTranslationPair ParseBankTranslationPair(Stream data);
    }
}