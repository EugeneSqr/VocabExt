using VX.Service.Infrastructure.Factories.Adapters;
using VX.Service.Infrastructure.JavaScriptConverters;

namespace VX.Service.Infrastructure.Factories.Converters
{
    public class JavaScroptConvertersFactory : ConverterFactory, 
        IConverterFactoryMethod<VocabBankTranslationConverter>,
        IConverterFactoryMethod<VocabBankHeadersConverter>,
        IConverterFactoryMethod<ParentChildIdPairConverter>,
        IConverterFactoryMethod<WordsConverter>
    {
        private readonly IAdapterFactory entitiesFactory;

        public JavaScroptConvertersFactory(IAdapterFactory entitiesFactory)
        {
            this.entitiesFactory = entitiesFactory;
        }

        VocabBankTranslationConverter IConverterFactoryMethod<VocabBankTranslationConverter>.Create()
        {
            return new VocabBankTranslationConverter(entitiesFactory);
        }

        VocabBankHeadersConverter IConverterFactoryMethod<VocabBankHeadersConverter>.Create()
        {
            return new VocabBankHeadersConverter(entitiesFactory);
        }

        ParentChildIdPairConverter IConverterFactoryMethod<ParentChildIdPairConverter>.Create()
        {
            return new ParentChildIdPairConverter();
        }

        WordsConverter IConverterFactoryMethod<WordsConverter>.Create()
        {
            return new WordsConverter(entitiesFactory);
        }
    }
}