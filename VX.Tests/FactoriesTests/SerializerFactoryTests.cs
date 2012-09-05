using System.IO;
using Autofac;
using Moq;
using VX.Domain.Entities;
using VX.Domain.Entities.Impl;
using VX.Domain.Surrogates;
using VX.Domain.Surrogates.Impl;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Tests.FactoriesTests
{
    public abstract class SerializerFactoryTests<TType, TImplementation> : DomainItemsCheckTests<TType, TImplementation>
    {
        protected ILanguage OutLanguage = new LanguageContract { Id = 1 };
        protected IWord OutWord = new WordContract { Id = 1 };
        protected ITranslation OutTranslation = new TranslationContract { Id = 1 };
        protected IVocabBankSummary OutVocabBankSummary = new VocabBankSummary { Id = 1 };


        protected readonly Stream CorrectStream = GetStream("abc");
        
        protected SerializerFactoryTests()
        {
            ContainerBuilder.RegisterInstance(MockContractSerializer()).SingleInstance();
        }

        private IContractSerializer MockContractSerializer()
        {
            var mock = new Mock<IContractSerializer>();

            mock.Setup(item => item.Deserialize<ILanguage, LanguageContract>(null, out OutLanguage))
                .Returns(false);
            mock.Setup(item => item.Deserialize<ILanguage, LanguageContract>(CorrectStream, out OutLanguage))
                .Returns(true);

            mock.Setup(item => item.Deserialize<IWord, WordContract>(null, out OutWord))
                .Returns(false);
            mock.Setup(item => item.Deserialize<IWord, WordContract>(CorrectStream, out OutWord))
                .Returns(true);

            mock.Setup(item => item.Deserialize<ITranslation, TranslationContract>(null, out OutTranslation))
                .Returns(false);
            mock.Setup(item => item.Deserialize<ITranslation, TranslationContract>(CorrectStream, out OutTranslation))
                .Returns(true);

            mock.Setup(item => item.Deserialize<IVocabBankSummary, VocabBankSummary>(null, out OutVocabBankSummary))
                .Returns(false);
            mock.Setup(item => item.Deserialize<IVocabBankSummary, VocabBankSummary>(CorrectStream, out OutVocabBankSummary))
                .Returns(true);

            return mock.Object;
        }
    }
}
