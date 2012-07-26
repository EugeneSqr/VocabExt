using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using VX.Domain.DataContracts;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Factories.Interfaces;
using VX.Service.Infrastructure.Interfaces;
using VX.Service.Infrastructure.JavaScriptConverters;

namespace VX.Service.Infrastructure
{
    public class InputDataConverter : IInputDataConverter
    {
        private readonly IEntitiesFactory entitiesFactory;

        public InputDataConverter(IEntitiesFactory entitiesFactory)
        {
            this.entitiesFactory = entitiesFactory;
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
            var serializer = new JavaScriptSerializer();
            try
            {
                var deserialized = serializer.Deserialize<Dictionary<string, int>>(new StreamReader(data).ReadToEnd());
                return new ParentChildIdPair(deserialized["parent"], deserialized["child"]);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        public IBankTranslationPair ParseBankTranslationPair(Stream data)
        {
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] {new BankTranslationConverter(entitiesFactory) });
            var deserialized = serializer.Deserialize<BankTranslationPair>(new StreamReader(data).ReadToEnd());
            return deserialized;
        }
    }
}