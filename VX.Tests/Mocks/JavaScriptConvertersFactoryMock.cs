using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Moq;
using VX.Domain.DataContracts;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Factories.Interfaces;
using VX.Service.Infrastructure;
using VX.Service.Infrastructure.Interfaces;
using VX.Service.Infrastructure.JavaScriptConverters;

namespace VX.Tests.Mocks
{
    public class JavaScriptConvertersFactoryMock : IJavaScriptConvertersFactory
    {
        public JavaScriptConverter Build(string converterName)
        {
            JavaScriptConverter result = null;
            switch (converterName)
            {
                case "VocabBankTranslationConverter":
                    result = MockVocabBankTranslationConverter();
                    break;
                case "VocabBankHeadersConverter":
                    result = MockVocabBankHeadersConverter();
                    break;
                case "ParentChildIdPairConverter":
                    result = MockParentChildIdConverter();
                    break;
            }

            return result;
        }

        private static JavaScriptConverter MockVocabBankTranslationConverter()
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

        private static JavaScriptConverter MockVocabBankHeadersConverter()
        {
            var mock = new Mock<VocabBankHeadersConverter>(new EntitiesFactoryMock());
            mock.Setup(item => item.Deserialize(
                It.IsAny<IDictionary<string, object>>(),
                It.IsAny<Type>(),
                It.IsAny<JavaScriptSerializer>()))
                .Returns(
                    (IDictionary<string, object> deserialized, Type type, JavaScriptSerializer serializer) =>
                    new VocabBankContract
                        {
                            Name = deserialized["Name"].ToString(),
                            Description = deserialized["Description"].ToString()
                        });
            mock.Setup(item => item.SupportedTypes).Returns(
                new[] { typeof(IVocabBank) });

            return mock.Object;
        }

        private static JavaScriptConverter MockParentChildIdConverter()
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
    }
}
