using Autofac;
using Moq;
using NUnit.Framework;
using VX.Domain.Entities;
using VX.Domain.Entities.Impl;
using VX.Domain.Responses.Impl;
using VX.Domain.Surrogates;
using VX.Service.Repositories;
using VX.Service.Repositories.Interfaces;
using VX.Service.Validators.Interfaces;

namespace VX.IntegrationTests.RepositoriesTests
{
    [TestFixture]
    internal class TranslationsRepositoryTest : DataLayerTestsBase<ITranslationsRepository, TranslationsRepository>
    {
        // This translation is correct and could be found by it's id
        private readonly ITranslation goodExistByIdTranslation = new TranslationContract
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

        // This translation is correct, it could not be found by it's id but could be found by source and target ids
        private readonly ITranslation goodNonExistByIdTranslation = new TranslationContract
                                                                    {
                                                                        Id = 9999,
                                                                        Source = new WordContract
                                                                                     {
                                                                                         Id = 1
                                                                                     },
                                                                        Target = new WordContract
                                                                                     {
                                                                                         Id = 2
                                                                                     }
                                                                    };


        // This translation is correct, but could not be found
        private readonly ITranslation goodNonExistTranslation = new TranslationContract
                                                                    {
                                                                        Id = 9999,
                                                                        Source = new WordContract
                                                                                     {
                                                                                         Id = 4
                                                                                     },
                                                                        Target = new WordContract
                                                                                     {
                                                                                         Id = 1
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
            IManyToManyRelationship updatedTranslation;
            ServiceOperationAction action;
            Assert.IsTrue(SystemUnderTest.SaveTranslation(goodExistByIdTranslation, out updatedTranslation, out action));
            Assert.AreEqual(ServiceOperationAction.Update, action);
            Assert.AreEqual(1, updatedTranslation.Id);
            Assert.AreEqual(1, updatedTranslation.SourceId);
            Assert.AreEqual(4, updatedTranslation.TargetId);
        }

        [Test]
        [Category("TranslationsRepositoryTests")]
        [Description("Checks if SaveTranslation creates translation correctly")]
        public void CreateTranslationTest()
        {
            IManyToManyRelationship createdTranslation;
            ServiceOperationAction action;
            Assert.IsTrue(SystemUnderTest.SaveTranslation(goodNonExistTranslation, out createdTranslation, out action));
            Assert.AreEqual(ServiceOperationAction.Create, action);
            Assert.AreNotEqual(0, createdTranslation.Id);
            Assert.AreEqual(4, createdTranslation.SourceId);
            Assert.AreEqual(1, createdTranslation.TargetId);
        }

        [Test]
        [Category("TranslationsRepositoryTests")]
        [Description("Checks if SaveTranslation finds translation by sourceId and targetId and updates it")]
        public void UpdateTranslationCompositeKeyTest()
        {
            IManyToManyRelationship updatedTranslation;
            ServiceOperationAction action;
            Assert.IsTrue(SystemUnderTest.SaveTranslation(goodNonExistByIdTranslation, out updatedTranslation, out action));
            Assert.AreEqual(ServiceOperationAction.Update, action);
            Assert.AreEqual(1, updatedTranslation.Id);
            Assert.AreEqual(1, updatedTranslation.SourceId);
            Assert.AreEqual(2, updatedTranslation.TargetId);
        }

        [Test]
        [Category("TranslationsRepositoryTests")]
        [Description("Checks if SaveTranslation rejects update because of source validation fails")]
        public void UpdateTranslationSourceValidationTest()
        {
            IManyToManyRelationship updatedTranslation;
            ServiceOperationAction action;
            Assert.IsFalse(SystemUnderTest.SaveTranslation(badSourceTranslation, out updatedTranslation, out action));
            Assert.AreEqual(null, updatedTranslation);
        }

        [Test]
        [Category("TranslationsRepositoryTests")]
        [Description("Checks if SaveTranslation rejects update because of target validation fails")]
        public void UpdateTranslationTargetValidationTest()
        {
            IManyToManyRelationship updatedTranslation;
            ServiceOperationAction action;
            Assert.IsFalse(SystemUnderTest.SaveTranslation(badTargetTranslation, out updatedTranslation, out action));
            Assert.AreEqual(null, updatedTranslation);
        }

        private ITranslationValidator MockTranslationValidator()
        {
            var mock = new Mock<ITranslationValidator>();
            mock.Setup(item => item.Validate(badSourceTranslation))
                .Returns(new ServiceOperationResponse(false, ServiceOperationAction.Validate));

            mock.Setup(item => item.Validate(badTargetTranslation))
                .Returns(new ServiceOperationResponse(false, ServiceOperationAction.Validate));

            mock.Setup(item => item.Validate(goodExistByIdTranslation))
                .Returns(new ServiceOperationResponse(true, ServiceOperationAction.Validate));

            mock.Setup(item => item.Validate(goodNonExistTranslation))
                .Returns(new ServiceOperationResponse(true, ServiceOperationAction.Validate));

            mock.Setup(item => item.Validate(goodNonExistByIdTranslation))
                .Returns(new ServiceOperationResponse(true, ServiceOperationAction.Validate));

            return mock.Object;
        }
    }
}