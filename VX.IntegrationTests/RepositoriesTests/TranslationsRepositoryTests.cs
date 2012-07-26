using System.ComponentModel.DataAnnotations;
using System.Linq;
using Autofac;
using Moq;
using NUnit.Framework;
using VX.Domain.DataContracts;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.CompositeValidators.Interfaces;
using VX.Service.Repositories;
using VX.Service.Repositories.Interfaces;

namespace VX.IntegrationTests.RepositoriesTests
{
    [TestFixture]
    internal class TranslationsRepositoryTest : RepositoryTestsBase<ITranslationsRepository, TranslationsRepository>
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
        
        public TranslationsRepositoryTest()
        {
            ContainerBuilder
                .RegisterInstance(MockTranslationValidator())
                .As<ITranslationValidator>()
                .SingleInstance();
            
            BuildContainer();
        }

        [Test]
        [Category("TranslationsRepositoryTests")]
        [Description("Checks if GetTranslations returns list of translations if parameters are correct")]
        public void GetTranslationsPositiveTest()
        {
            var actual = SystemUnderTest.GetTranslations(1);
            Assert.Greater(actual.Count, 0);
        }

        [Test]
        [Category("TranslationsRepositoryTests")]
        [Description("Checks if GetTranslations returns empty list if id is incorrect")]
        public void GetTranslationsNegativeBadIdTest()
        {
            var actual = SystemUnderTest.GetTranslations(-22);
            Assert.AreEqual(0, actual.Count);
        }

        [Test]
        [Category("TranslationsRepositoryTests")]
        [Description("Checks of SaveTranslation updates translation correctly")]
        public void UpdateTranslationTest()
        {
            Assert.IsTrue(SystemUnderTest.SaveTranslation(goodTranslation, 1).Status);
            var actual = SystemUnderTest.GetTranslations(1).First(item => item.Id == 1);
            Assert.AreEqual(1, actual.Source.Id);
            Assert.AreEqual(4, actual.Target.Id);
        }

        [Test]
        [Category("TranslationsRepositoryTests")]
        [Description("Checks if SaveTranslation rejects update because of source validation fails")]
        public void UpdateTranslationSourceValidationTest()
        {
            Assert.IsFalse(SystemUnderTest.SaveTranslation(badSourceTranslation, 1).Status);
        }

        [Test]
        [Category("TranslationsRepositoryTests")]
        [Description("Checks if SaveTranslation rejects update because of target validation fails")]
        public void UpdateTranslationTargetValidationTest()
        {
            Assert.IsFalse(SystemUnderTest.SaveTranslation(badTargetTranslation, 1).Status);
        }

        private ITranslationValidator MockTranslationValidator()
        {
            var mock = new Mock<ITranslationValidator>();
            mock.Setup(item => item.Validate(badSourceTranslation))
                .Returns(new ValidationResult("bad source"));

            mock.Setup(item => item.Validate(badTargetTranslation))
                .Returns(new ValidationResult("bad target"));

            mock.Setup(item => item.Validate(goodTranslation))
                .Returns(ValidationResult.Success);

            return mock.Object;
        }
    }
}