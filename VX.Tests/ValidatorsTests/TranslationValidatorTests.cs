using Autofac;
using Moq;
using NUnit.Framework;
using VX.Domain;
using VX.Domain.DataContracts;
using VX.Domain.DataContracts.Interfaces;
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
            CheckValidationResult(false, null, SystemUnderTest.Validate(badSourceTranslation));
        }

        [Test]
        [Category("TranslationValidatorTests")]
        [Description("Cheks if validation fails on bad target input")]
        public void ValidateTargetFailTest()
        {
            CheckValidationResult(false, null, SystemUnderTest.Validate(badTargetTranslation));
        }

        private IWordValidator MockWordValidator()
        {
            var mock = new Mock<IWordValidator>();
            mock.Setup(item => item.ValidateExist(ExistWordFirst))
                .Returns(new ServiceOperationResponse(true, ServiceOperationAction.Validate));
            mock.Setup(item => item.ValidateExist(ExistWordSecond))
                .Returns(new ServiceOperationResponse(true, ServiceOperationAction.Validate));
            mock.Setup(item => item.ValidateExist(NonExistWord))
                .Returns(new ServiceOperationResponse(false, ServiceOperationAction.Validate));

            return mock.Object;
        }
    }
}
