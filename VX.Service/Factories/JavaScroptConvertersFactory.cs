using System.Web.Script.Serialization;
using VX.Service.Factories.Interfaces;
using VX.Service.Infrastructure.JavaScriptConverters;

namespace VX.Service.Factories
{
    public class JavaScroptConvertersFactory : IJavaScriptConvertersFactory
    {
        private readonly IEntitiesFactory entitiesFactory;

        public JavaScroptConvertersFactory(IEntitiesFactory entitiesFactory)
        {
            this.entitiesFactory = entitiesFactory;
        }

        public JavaScriptConverter Build(string converterName)
        {
            JavaScriptConverter result = null;
            switch (converterName)
            {
                case "VocabBankTranslationConverter":
                    result = new VocabBankTranslationConverter(entitiesFactory);
                    break;
                case "VocabBankHeadersConverter":
                    result = new VocabBankHeadersConverter(entitiesFactory);
                    break;
                case "ParentChildIdPairConverter":
                    result = new ParentChildIdPairConverter();
                    break;
            }

            return result;
        }
    }
}