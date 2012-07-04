using System.IO;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Service.Infrastructure.Interfaces
{
    public interface IInputDataConverter
    {
        int EmptyId { get; }

        int Convert(string id);

        ITranslation Convert(Stream data);

        IParentChildIdPair ParsePair(Stream data);
    }
}