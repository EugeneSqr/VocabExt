using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure
{
    [RegisterService]
    public class ContractSerializer : IContractSerializer
    {
        public bool Deserialize<TType, TContract>(Stream data, out TType result)
        {
            result = default(TType);
            try
            {
                result = (TType) new DataContractJsonSerializer(typeof (TContract)).ReadObject(data);
                return true;
            }
            catch (SerializationException)
            {
                return false;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }
    }
}