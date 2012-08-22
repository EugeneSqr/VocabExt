using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Moq;
using VX.Domain.DataContracts;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Infrastructure;
using VX.Service.Infrastructure.Factories.Converters;
using VX.Service.Infrastructure.Interfaces;
using VX.Service.Infrastructure.JavaScriptConverters;

namespace VX.Tests.Mocks
{
    public class JavaScriptConvertersFactoryMock : ConverterFactory,
        IConverterFactoryMethod<VocabBankTranslationConverter>,
        IConverterFactoryMethod<VocabBankHeadersConverter>,
        IConverterFactoryMethod<ParentChildIdPairConverter>,
        IConverterFactoryMethod<WordsConverter>
    {
        VocabBankTranslationConverter IConverterFactoryMethod<VocabBankTranslationConverter>.Create()
        {
            return MockVocabBankTranslationConverter();
        }

        VocabBankHeadersConverter IConverterFactoryMethod<VocabBankHeadersConverter>.Create()
        {
            return MockVocabBankHeadersConverter();
        }

        ParentChildIdPairConverter IConverterFactoryMethod<ParentChildIdPairConverter>.Create()
        {
            return MockParentChildIdConverter();
        }

        WordsConverter IConverterFactoryMethod<WordsConverter>.Create()
        {
            return MockWordsConverter();
        }

        private static VocabBankTranslationConverter MockVocabBankTranslationConverter()
        {
            var mock = new Mock<VocabBankTranslationConverter>(new EntitiesFactoryMock());
            mock.Setup(item => item.Deserialize(
                It.IsAny<IDictionary<string, object>>(),
                It.IsAny<Type>(),
                It.IsAny<JavaScriptSerializer>()))
                .Returns((IDictionary<string, object> deserialized, Type type, JavaScriptSerializer serializer) =>
                             {
                                 var translation = (IDictionary<string, object>)deserialized["Translation"];
                                 return new BankTranslationPair(
                                     (int)deserialized["VocabBankId"],
                                     new TranslationContract
                                         {
                                             Id = (int)translation["Id"]
                                         });
                             });
            mock.Setup(item => item.SupportedTypes).Returns(
                new[] {typeof (IBankTranslationPair)});
            return mock.Object;
        }

        private static VocabBankHeadersConverter MockVocabBankHeadersConverter()
        {
            var mock = new Mock<VocabBankHeadersConverter>(new EntitiesFactoryMock());
            mock.Setup(item => item.Deserialize(
                It.IsAny<IDictionary<string, object>>(),
                It.IsAny<Type>(),
                It.IsAny<JavaScriptSerializer>()))
                .Returns(
                    (IDictionary<string, object> deserialized, Type type, JavaScriptSerializer serializer) =>
                    new EntitiesFactoryMock().Create<IVocabBank, IDictionary<string, object>>(deserialized));

            mock.Setup(item => item.SupportedTypes).Returns(
                new[] {typeof (IVocabBank)});

            return mock.Object;
        }

        private static ParentChildIdPairConverter MockParentChildIdConverter()
        {
            var mock = new Mock<ParentChildIdPairConverter>();
            mock.Setup(item => item.Deserialize(It.IsAny<IDictionary<string, object>>(),
                                                It.IsAny<Type>(),
                                                It.IsAny<JavaScriptSerializer>()))
                .Returns(
                    (IDictionary<string, object> deserialized, Type type, JavaScriptSerializer serializer) =>
                    new ParentChildIdPair(
                        int.Parse(deserialized["parent"].ToString()), 
                        int.Parse(deserialized["child"].ToString())));
            mock.Setup(item => item.SupportedTypes).Returns(new[] {typeof (IParentChildIdPair)});

            return mock.Object;
        }

        private static WordsConverter MockWordsConverter()
        {
            var mock = new Mock<WordsConverter>(new EntitiesFactoryMock());
            mock.Setup(item => item.Deserialize(
                It.IsAny<IDictionary<string, object>>(),
                It.IsAny<Type>(),
                It.IsAny<JavaScriptSerializer>()))
                .Returns((IDictionary<string, object> deserialized, Type type, JavaScriptSerializer serializer) =>
                         new EntitiesFactoryMock().Create<IWord, IDictionary<string, object>>(deserialized));

            mock.Setup(item => item.SupportedTypes).Returns(new[] {typeof (IWord)});

            return mock.Object;
        }
    }
}
