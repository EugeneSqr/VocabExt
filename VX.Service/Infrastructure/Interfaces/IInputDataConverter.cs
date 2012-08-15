using System.IO;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Service.Infrastructure.Interfaces
{
    public interface IInputDataConverter
    {
        int EmptyId { get; }

        int Convert(string id);

        IParentChildIdPair ParsePair(Stream data);

        IBankTranslationPair ParseBankTranslationPair(Stream data);

        IVocabBank ParseBankHeaders(Stream data);

        IWord ParseWord(Stream data);
    }
}