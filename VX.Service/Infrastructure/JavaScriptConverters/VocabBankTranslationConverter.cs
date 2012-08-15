using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using VX.Service.Factories.Interfaces;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure.JavaScriptConverters
{
    public class VocabBankTranslationConverter : EntitiesConverter
    {
        public VocabBankTranslationConverter(IEntitiesFactory entitiesFactory) : base(entitiesFactory)
        {
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return new[] {typeof(IBankTranslationPair)};}
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            var translation = (IDictionary<string, object>)dictionary["Translation"];
            return new BankTranslationPair(
                (int)dictionary["VocabBankId"],
                EntitiesFactory.BuildTranslation(translation));
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}