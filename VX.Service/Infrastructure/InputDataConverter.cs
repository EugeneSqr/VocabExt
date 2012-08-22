using System;
using System.IO;
using System.Web.Script.Serialization;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Infrastructure.Factories.Converters;
using VX.Service.Infrastructure.Interfaces;
using VX.Service.Infrastructure.JavaScriptConverters;

namespace VX.Service.Infrastructure
{
    public class InputDataConverter : IInputDataConverter
    {
        private readonly IConverterFactory convertersFactory;

        public InputDataConverter(IConverterFactory convertersFactory)
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
            return Parse<IParentChildIdPair, ParentChildIdPairConverter>(data);
        }

        public IBankTranslationPair ParseBankTranslationPair(Stream data)
        {
            return Parse<IBankTranslationPair, VocabBankTranslationConverter>(data);
        }

        public IVocabBank ParseBankHeaders(Stream data)
        {
            return Parse<IVocabBank, VocabBankHeadersConverter>(data);
        }

        public IWord ParseWord(Stream data)
        {
            return Parse<IWord, WordsConverter>(data);
        }

        private T Parse<T, TConverter>(Stream data) where TConverter : JavaScriptConverter
        {
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] {convertersFactory.Create<TConverter>()});
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