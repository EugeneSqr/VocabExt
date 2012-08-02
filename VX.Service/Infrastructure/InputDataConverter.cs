using System;
using System.IO;
using System.Web.Script.Serialization;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Factories.Interfaces;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure
{
    public class InputDataConverter : IInputDataConverter
    {
        private readonly IJavaScriptConvertersFactory convertersFactory;

        public InputDataConverter(IJavaScriptConvertersFactory convertersFactory)
        {
            this.convertersFactory = convertersFactory;
        }

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

        public IParentChildIdPair ParsePair(Stream data)
        {
            return Parse<IParentChildIdPair>(data, "ParentChildIdPairConverter");
        }

        public IBankTranslationPair ParseBankTranslationPair(Stream data)
        {
            return Parse<IBankTranslationPair>(data, "VocabBankTranslationConverter");
        }

        public IVocabBank ParseBankHeaders(Stream data)
        {
            return Parse<IVocabBank>(data, "VocabBankHeadersConverter");
        }

        private T Parse<T>(Stream data, string converterName)
        {
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] {convertersFactory.Build(converterName)});
            try
            {
                return serializer.Deserialize<T>(new StreamReader(data).ReadToEnd());
            }
            catch (ArgumentException)
            {
                return default(T);
            }
        }
    }
}