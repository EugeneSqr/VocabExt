using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure.JavaScriptConverters
{
    public class ParentChildIdPairConverter : JavaScriptConverter
    {
        public override IEnumerable<Type> SupportedTypes
        {
            get { return new[] {typeof(IParentChildIdPair)}; }
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            return new ParentChildIdPair(
                UnboxStringToInt(dictionary["parent"]), 
                UnboxStringToInt(dictionary["child"]));
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private int UnboxStringToInt(object boxedString)
        {
            return int.Parse(boxedString.ToString());
        }
    }
}