using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Infrastructure.Factories.Adapters;

namespace VX.Service.Infrastructure.JavaScriptConverters
{
    public class WordsConverter : EntitiesConverter
    {
        public WordsConverter(IAdapterFactory entitiesFactory) : base(entitiesFactory)
        {
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return new[] {typeof (IWord)}; }
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            return EntitiesFactory.Create<IWord, IDictionary<string, object>>(dictionary);
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}