using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Factories.Interfaces;

namespace VX.Service.Infrastructure.JavaScriptConverters
{
    public class WordsConverter : EntitiesConverter
    {
        public WordsConverter(IEntitiesFactory entitiesFactory) : base(entitiesFactory)
        {
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return new[] {typeof (IWord)}; }
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            return EntitiesFactory.BuildWord(dictionary);
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}