using Autofac;
using Moq;
using NUnit.Framework;
using VX.Domain.Entities;
using VX.Domain.Entities.Impl;
using VX.Service.Repositories.Interfaces;
using VX.Service.Validators;
using VX.Service.Validators.Interfaces;

namespace VX.Tests.ValidatorsTests
{
    [TestFixture]
    internal class WordValidatorTests : ValidatorTestsBase<IWordValidator, WordValidator>
    {
        private const int ExistId = 1;
        private const int NonExistId = -5;
        private const string ExistSpelling = "arrival";
        private const string NonExistSpelling = "test_spelling";

        private readonly IWordsRepository wordsRepository = MockWordsRepository();
        
        public WordValidatorTests()
        {
            ContainerBuilder.RegisterInstance(MockLanguageRepository()).SingleInstance();
            BuildContainer();
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if ValidateSpelling returns true if input correct")]
        public void ValidateSpellingTest()
        {
            CheckValidationResult(
                true, 
                null,
                SystemUnderTest.ValidateSpelling(new WordContract { Spelling = "test" }));
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if ValidateSpelling returns false and error message 0 if word is null")]
        public void ValidateSpellingWordNullTest()
        {
            CheckValidationResult(false, "0", SystemUnderTest.ValidateSpelling(null));
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if ValidateSpelling returns false and error message 1 if word's spelling is null")]
        public void ValidateSpellingNullTest()
        {
            CheckValidationResult(
                false, 
                "1", 
                SystemUnderTest.ValidateSpelling(new WordContract { Spelling = null }));
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if ValidateSpelling returns false and error message 1 if word's spelling is empty")]
        public void ValidateSpellingEmptyTest()
        {
            CheckValidationResult(
                false,
                "1",
                SystemUnderTest.ValidateSpelling(new WordContract {Spelling = string.Empty}));
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if ValidateSpelling returns false and error message 1 if word's spelling is spaces only")]
        public void ValidateSpellingSpacesTest()
        {
            CheckValidationResult(
                false,
                "1",
                SystemUnderTest.ValidateSpelling(new WordContract { Spelling = "   "}));
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if ValidateLanguage returns false and error code 2 if language is null")]
        public void ValidateLanguageNullTest()
        {
            CheckValidationResult(
                false,
                "2",
                SystemUnderTest.ValidateLanguage(new WordContract {Language = null}));
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if ValidateLanguage returns false end error code 2 if language doesn't exist")]
        public void ValidateLangugageDoesntExistTest()
        {
            CheckValidationResult(
                false,
                "2",
                SystemUnderTest.ValidateLanguage(new WordContract
                                                     {
                                                         Language = new LanguageContract {Id = NonExistId}
                                                     }));
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if ValidateLanguage returns true if language exists")]
        public void ValidateLanguageTest()
        {
            CheckValidationResult(
                true,
                null,
                SystemUnderTest.ValidateLanguage(new WordContract
                                                     {
                                                         Language = new LanguageContract {Id = ExistId}
                                                     }));
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if ValidateLanguage returns false if word is null")]
        public void ValidateLanguageWordNullTest()
        {
            CheckValidationResult(false, "0", SystemUnderTest.ValidateLanguage(null));
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if ValidateExist returns false if word is null")]
        public void ValidateExistNullTest()
        {
            CheckValidationResult(false, "0", SystemUnderTest.ValidateExist(null, wordsRepository));
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if ValidateExist returns false if word already exists (by id)")]
        public void ValidateExistIdNegativeTest()
        {
            var actual = SystemUnderTest.ValidateExist(new WordContract {Id = ExistId}, wordsRepository);
            CheckValidationResult(false, "3", actual);
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if ValidateExist returns false if word already exists (by id and spelling)")]
        public void ValidateExistIdSpellingNegativeTest()
        {
            CheckValidationResult(
                false,
                "3",
                SystemUnderTest.ValidateExist(
                    new WordContract { Id = ExistId, Spelling = ExistSpelling}, wordsRepository));
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if ValidateExist returns false if word already exists (by spelling)")]
        public void ValidateExistSpellingTest()
        {
            CheckValidationResult(
                false,
                "3",
                SystemUnderTest.ValidateExist(
                    new WordContract {Id = NonExistId, Spelling = ExistSpelling}, wordsRepository));
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if ValidateExist return true id word doesn't exist")]
        public void ValidateExistPositiveTest()
        {
            CheckValidationResult(
                true, 
                null, 
                SystemUnderTest.ValidateExist(
                    new WordContract {Id = NonExistId, Spelling = NonExistSpelling}, wordsRepository));
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if Validate returns false if word is null")]
        public void ValidateWordNullTest()
        {
            CheckValidationResult(false, "0", SystemUnderTest.Validate(null, wordsRepository));
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if Validate returns true if word is correct")]
        public void ValidateTest()
        {
            CheckValidationResult(
                true,
                null,
                SystemUnderTest.Validate(new WordContract
                                             {
                                                 Id = NonExistId,
                                                 Spelling = NonExistSpelling,
                                                 Language = new LanguageContract
                                                                {
                                                                    Id = ExistId
                                                                }
                                             }, wordsRepository));
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if Validate returns false if ValidateSpelling false")]
        public void ValidateValidateSpellingTest()
        {
            CheckValidationResult(
                false,
                "1",
                SystemUnderTest.Validate(new WordContract
                                             {
                                                 Spelling = null
                                             }, wordsRepository));
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if Validate returns false if ValidateLanguage false")]
        public void ValidateValidateLanguageTest()
        {
            CheckValidationResult(
                false,
                "2",
                SystemUnderTest.Validate(new WordContract
                                             {
                                                 Spelling = NonExistSpelling,
                                                 Language = new LanguageContract
                                                                {
                                                                    Id = NonExistId
                                                                }
                                             }, wordsRepository));
        }

        [Test]
        [Category("WordValidatorTests")]
        [Description("Checks if Validate returns false if ValidateExist false")]
        public void ValidateValidateExistTest()
        {
            CheckValidationResult(
                false,
                "3",
                SystemUnderTest.Validate(new WordContract
                                             {
                                                 Id = ExistId,
                                                 Spelling = NonExistSpelling,
                                                 Language = new LanguageContract
                                                                {
                                                                    Id = ExistId
                                                                }
                                             }, wordsRepository));
        }

        private static ILanguagesRepository MockLanguageRepository()
        {
            var mock = new Mock<ILanguagesRepository>();
            mock.Setup(item => item.GetLanguage(ExistId)).Returns(new LanguageContract());
            mock.Setup(item => item.GetLanguage(NonExistId)).Returns((ILanguage)null);
            return mock.Object;
        }

        private static IWordsRepository MockWordsRepository()
        {
            var mock = new Mock<IWordsRepository>();
            mock.Setup(item => item.GetWord(ExistId)).Returns(new WordContract {Id = ExistId});
            mock.Setup(item => item.GetWord(NonExistId)).Returns(new WordContract());
            mock.Setup(item => item.CheckWordExists(ExistSpelling)).Returns(true);
            mock.Setup(item => item.CheckWordExists(NonExistSpelling)).Returns(false);
            return mock.Object;
        }
    }
}
