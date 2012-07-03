using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using VX.Domain.DataContracts;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure
{
    public class InputDataConverter : IInputDataConverter
    {
        public int EmptyId
        {
            get { return -1; }
        }

        public int Convert(string id)
        {
            int result;
            return int.TryParse(id, out result) 
                ? result 
                : EmptyId;
        }

        public ITranslation Convert(Stream data)
        {
            var serializer = new DataContractJsonSerializer(typeof (TranslationContract));
            try
            {
                return (ITranslation)serializer.ReadObject(data);
            }
            catch (SerializationException)
            {
                return null;
            }
        }
    }
}