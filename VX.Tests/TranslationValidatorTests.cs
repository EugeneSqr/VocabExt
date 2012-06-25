using System.ComponentModel.DataAnnotations;
using Autofac;
using Moq;
using NUnit.Framework;
using VX.Domain.DataContracts;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.CompositeValidators;
using VX.Service.CompositeValidators.Interfaces;
using VX.Service.Repositories.Interfaces;
using VX.Tests.RepositoriesTests;

namespace VX.Tests
{
    [TestFixture]
    internal class TranslationValidatorTests : RepositoryTestsBase<ITranslationValidator, TranslationValidator>
    {
        private readonly ITranslation goodTranslation = new TranslationContract
                                                            {
                                                                Id = 1,
                                                                Source = new WordContract
                                                                             {
                                                                                 Id = 1
                                                                             },
                                                                Target = new WordContract
                                                                             {
                                                                                 Id = 4
                                                                             }
                                                            };

        private readonly ITranslation badSourceTranslation = new TranslationContract
                                                                 {
                                                                     Id = 1,
                                                                     Source = new WordContract
                                                                                  {
                                                                                      Id = -999
                                                                                  },
                                                                     Target = new WordContract
                                                                                  {
                                                                                      Id = 4
                                                                                  }
                                                                 };

        private readonly ITranslation badTargetTranslation = new TranslationContract
                                                                 {
                                                                     Id = 1,
                                                                     Source = new WordContract
                                                                                  {
                                                                                      Id = 1
                                                                                  },
                                                                     Target = new WordContract
                                                                                  {
                                                                                      Id = -999
                                                                                  }
                                                                 };
        
        public TranslationValidatorTests()
        {
            ContainerBuilder.RegisterInstance(MockWordsRepository())
                .As<IWordsRepository>()
                .SingleInstance();
            
            BuildContainer();
        }

        [Test]
        [Category("TranslationValidatorTests")]
        [Description("Checks if validation passes on good input")]
        public void ValidatePassTest()
        {
            Assert.AreEqual(ValidationResult.Success, SystemUnderTest.Validate(goodTranslation)); 
        }

        [Test]
        [Category("TranslationValidatorTests")]
        [Description("Checks if validation fails on bad source input")]
        public void ValidateSourceFailTest()
        {
            Assert.AreNotEqual(ValidationResult.Success, SystemUnderTest.Validate(badSourceTranslation));
        }

        [Test]
        [Category("TranslationValidatorTests")]
        [Description("Cheks if validation fails on bad target input")]
        public void ValidateTargetFailTest()
        {
            Assert.AreNotEqual(ValidationResult.Success, SystemUnderTest.Validate(badTargetTranslation));
        }

        private IWordsRepository MockWordsRepository()
        {
            var mock = new Mock<IWordsRepository>();
            mock.Setup(item => item.GetWord(goodTranslation.Source.Id)).Returns(goodTranslation.Source);
            mock.Setup(item => item.GetWord(goodTranslation.Target.Id)).Returns(goodTranslation.Target);
            mock.Setup(item => item.GetWord(badTargetTranslation.Target.Id)).Returns((WordContract)null);
            return mock.Object;
        }
    }
}
