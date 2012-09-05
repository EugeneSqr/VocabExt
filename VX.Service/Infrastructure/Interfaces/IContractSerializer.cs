using System.IO;

namespace VX.Service.Infrastructure.Interfaces
{
    public interface IContractSerializer
    {
        bool Deserialize<TType, TContract>(Stream data, out TType result);
    }
}