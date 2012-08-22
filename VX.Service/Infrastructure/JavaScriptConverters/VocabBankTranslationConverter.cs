using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Infrastructure.Factories.Adapters;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure.JavaScriptConverters
{
    public class VocabBankTranslationConverter : EntitiesConverter
    {
        public VocabBankTranslationConverter(IAdapterFactory entitiesFactory) : base(entitiesFactory)
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
                EntitiesFactory.Create<ITranslation, IDictionary<string, object>>(translation));
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}