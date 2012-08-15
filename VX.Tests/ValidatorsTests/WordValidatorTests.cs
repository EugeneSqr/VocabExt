using Autofac;
using Moq;
using NUnit.Framework;
using VX.Domain;
using VX.Domain.DataContracts;
using VX.Service.Factories.Interfaces;
using VX.Service.Repositories.Interfaces;
using VX.Service.Validators;
using VX.Service.Validators.Interfaces;

namespace VX.Tests.ValidatorsTests
{
    [TestFixture]
    internal class WordValidatorTests : DataLayerTestsBase<IWordValidator, WordValidator>
    {
        public WordValidatorTests()
        {
            ContainerBuilder.RegisterInstance(new Mock<IWordsRepository>().Object).SingleInstance();
            ContainerBuilder.RegisterInstance(new Mock<ILanguagesRepository>().Object).SingleInstance();
            ContainerBuilder.RegisterInstance(MockServiceOperationResponseFactory()).SingleInstance();
            BuildContainer();
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if ValidateSpelling returns true if input correct")]
        public void ValidateSpellingTest()
        {
            Assert.IsTrue(SystemUnderTest.ValidateSpelling(new WordContract
                                                               {
                                                                   Spelling = "test"
                                                               }).Status);
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if ValidateSpelling returns false if word is null")]
        public void ValidateSpellingWordNullTest()
        {
            Assert.IsFalse(SystemUnderTest.ValidateSpelling(null).Status);
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if ValidateSpelling returns false if word's spelling is null")]
        public void ValidateSpellingNullTest()
        {
            Assert.IsFalse(SystemUnderTest.ValidateSpelling(new WordContract
                                                                {
                                                                   Spelling = null
                                                                }).Status);
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if ValidateSpelling returns false if word's spelling is empty")]
        public void ValidateSpellingEmptyTest()
        {
            Assert.IsFalse(SystemUnderTest.ValidateSpelling(new WordContract
                                                                {
                                                                    Spelling = string.Empty
                                                                }).Status);
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if ValidateSpelling returns false if word's spelling is spaces only")]
        public void ValidateSpellingSpacesTest()
        {
            Assert.IsFalse(SystemUnderTest.ValidateSpelling(new WordContract
                                                                {
                                                                    Spelling = "   "
                                                                }).Status);
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if Validate returns false if word is null")]
        public void ValidateNullTest()
        {
            Assert.IsFalse(SystemUnderTest.Validate(null).Status);
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if ValidateLanguage returns false if word is null")]
        public void ValidateLanguageTest()
        {
            Assert.IsFalse(SystemUnderTest.ValidateLanguage(null).Status);
        }

        private static IServiceOperationResponseFactory MockServiceOperationResponseFactory()
        {
            var mock = new Mock<IServiceOperationResponseFactory>();
            mock.Setup(item => item.Build(It.IsAny<bool>(), It.IsAny<ServiceOperationAction>()))
                .Returns((bool status, ServiceOperationAction serviceOperationAction) => 
                    new ServiceOperationResponse(status, serviceOperationAction));
            mock.Setup(item => item.Build(It.IsAny<bool>(), It.IsAny<ServiceOperationAction>(), It.IsAny<string>()))
                .Returns((bool status, ServiceOperationAction serviceOperationAction, string message) => 
                    new ServiceOperationResponse(status, serviceOperationAction));

            return mock.Object;
        }
    }
}
