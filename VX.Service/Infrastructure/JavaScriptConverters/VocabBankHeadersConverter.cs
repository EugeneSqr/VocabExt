using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Infrastructure.Factories.Adapters;

namespace VX.Service.Infrastructure.JavaScriptConverters
{
    public class VocabBankHeadersConverter : EntitiesConverter
    {
        public VocabBankHeadersConverter(IAdapterFactory entitiesFactory) : base(entitiesFactory)
        {
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return new[] { typeof(IVocabBank) }; }
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            // TODO: to another factory
            return EntitiesFactory.Create<IVocabBank, IDictionary<string, object>>(dictionary);
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}