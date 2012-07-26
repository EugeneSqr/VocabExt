using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using VX.Service.Factories.Interfaces;

namespace VX.Service.Infrastructure.JavaScriptConverters
{
    public class BankTranslationConverter : JavaScriptConverter
    {
        private readonly IEntitiesFactory entitiesFactory;

        public BankTranslationConverter(IEntitiesFactory entitiesFactory)
        {
            this.entitiesFactory = entitiesFactory;
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            var translation = (IDictionary<string, object>)dictionary["Translation"];
            return new BankTranslationPair(
                (int)dictionary["VocabBankId"],
                entitiesFactory.BuildTranslation(translation));
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return new[] {typeof(BankTranslationPair)};}
        }
    }
}