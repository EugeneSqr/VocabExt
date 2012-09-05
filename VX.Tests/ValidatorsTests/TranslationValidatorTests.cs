using Autofac;
using Moq;
using NUnit.Framework;
using VX.Domain;
using VX.Domain.Entities;
using VX.Domain.Entities.Impl;
using VX.Domain.Responses.Impl;
using VX.Domain.Surrogates;
using VX.Domain.Surrogates.Impl;
using VX.Service.Repositories.Interfaces;
using VX.Service.Validators;
using VX.Service.Validators.Interfaces;

namespace VX.Tests.ValidatorsTests
{
    [TestFixture]
    internal class TranslationValidatorTests : ValidatorTestsBase<ITranslationValidator, TranslationValidator>
    {
        private static readonly IWord ExistWordFirst = new WordContract
                                                    {
                                                        Id = 1
                                                    };

        private static readonly IWord ExistWordSecond = new WordContract
                                                     {
                                                         Id = 4
                                                     };

        private static readonly IWord NonExistWord = new WordContract
                                                  {
                                                      Id = -999
                                                  };
        
        private readonly ITranslation goodTranslation = new TranslationContract
                                                            {
                                                                Id = 1,
                                                                Source = ExistWordFirst,
                                                                Target = ExistWordSecond
                                                            };

        private readonly ITranslation badSourceTranslation = new TranslationContract
                                                                 {
                                                                     Id = 1,
                                                                     Source = NonExistWord,
                                                                     Target = ExistWordSecond
                                                                 };

        private readonly ITranslation badTargetTranslation = new TranslationContract
                                                                 {
                                                                     Id = 1,
                                                                     Source = ExistWordFirst,
                                                                     Target = NonExistWord
                                                                 };
        
        public TranslationValidatorTests()
        {
            ContainerBuilder.RegisterInstance(MockWordValidator())
                .As<IWordValidator>()
                .SingleInstance();
            ContainerBuilder.RegisterInstance(new Mock<IWordsRepository>().Object)
                .As<IWordsRepository>()
                .SingleInstance();
            
            BuildContainer();
        }

        [Test]
        [Category("TranslationValidatorTests")]
        [Description("Checks if validation passes on good input")]
        public void ValidatePassTest()
        {
            CheckValidationResult(true, null, SystemUnderTest.Validate(goodTranslation));
        }

        [Test]
        [Category("TranslationValidatorTests")]
        [Description("Checks if validation fails on bad source input")]
        public void ValidateSourceFailTest()
        {
            CheckValidationResult(false, "Source word does not exist", SystemUnderTest.Validate(badSourceTranslation));
        }

        [Test]
        [Category("TranslationValidatorTests")]
        [Description("Cheks if validation fails on bad target input")]
        public void ValidateTargetFailTest()
        {
            CheckValidationResult(false, "Target word does not exist", SystemUnderTest.Validate(badTargetTranslation));
        }

        private static IWordValidator MockWordValidator()
        {
            var mock = new Mock<IWordValidator>();
            mock.Setup(item => item.ValidateExist(ExistWordFirst, It.IsAny<IWordsRepository>()))
                .Returns(new ServiceOperationResponse(false, ServiceOperationAction.Validate));
            mock.Setup(item => item.ValidateExist(ExistWordSecond, It.IsAny<IWordsRepository>()))
                .Returns(new ServiceOperationResponse(false, ServiceOperationAction.Validate));
            mock.Setup(item => item.ValidateExist(NonExistWord, It.IsAny<IWordsRepository>()))
                .Returns(new ServiceOperationResponse(true, ServiceOperationAction.Validate));

            return mock.Object;
        }
    }
}
