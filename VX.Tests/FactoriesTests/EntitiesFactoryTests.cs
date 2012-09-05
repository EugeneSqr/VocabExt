using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.IO;
using NUnit.Framework;
using VX.Domain.Entities;
using VX.Domain.Surrogates;
using VX.Model;
using VX.Service.Infrastructure.Factories.Entities;

namespace VX.Tests.FactoriesTests
{
    [TestFixture]
    internal class EntitiesFactoryTests : SerializerFactoryTests<IAbstractEntitiesFactory, EntitiesFactory>
    {
        private readonly Language testLanguage;
        private readonly Word testWord;
        private readonly Translation testTranslation;
        private readonly VocabBank testVocabBank;
        private readonly Tag testTag;
        
        public EntitiesFactoryTests()
        {
            BuildContainer();

            testLanguage = new Language {Id = 1, Name = "english", Abbreviation = "en"};
            testWord = new Word {Id = 1, Spelling = "spelling", Transcription = "transcription", Language = testLanguage};
            testTranslation = new Translation {Id = 1, Source = testWord, Target = testWord};
            testVocabBank = new VocabBank
                                {
                                    Id = 1,
                                    Description = "description",
                                    Name = "name",
                                    VocabBanksTranslations = new EntityCollection<VocabBanksTranslation>
                                                                 {
                                                                     new VocabBanksTranslation
                                                                         {
                                                                             Translation = testTranslation
                                                                         }
                                                                 }
                                };
            testTag = new Tag{Id = 1, Name = "name", Description = "description"};
        }

        #region EntitiesFactory.Stream

        [Test]
        [Category("EntitiesFactory.Entities.Stream")]
        [Description("Creates language from correct stream")]
        public void CreateLanguageCorrectStreamTest()
        {
            var actual = SystemUnderTest.Create<ILanguage, Stream>(CorrectStream);
            Assert.AreEqual(OutLanguage.Id, actual.Id);
        }

        [Test]
        [Category("EntitiesFactory.Entities.Stream")]
        [Description("Creates empty language from incorrect stream")]
        public void CreateEmptyLanguageIncorrectStreamTest()
        {
            CheckEmptyLanguage(SystemUnderTest.Create<ILanguage, Stream>(null));
        }

        [Test]
        [Category("EntitiesFactory.Entities.Stream")]
        [Description("Creates word from correct stream")]
        public void CreateWordCorrectStreamTest()
        {
            var actual = SystemUnderTest.Create<IWord, Stream>(CorrectStream);
            Assert.AreEqual(OutWord.Id, actual.Id);
        }

        [Test]
        [Category("EntitiesFactory.Entities.Stream")]
        [Description("Creates empty word from incorrect stream")]
        public void CreateEmptyWordIncorrectStreamTest()
        {
            CheckEmptyWord(SystemUnderTest.Create<IWord, Stream>(null));
        }

        [Test]
        [Category("EntitiesFactory.Entities.Stream")]
        [Description("Creates translation from correct stream")]
        public void CreateTranslationCorrectStreamTest()
        {
            var actual = SystemUnderTest.Create<ITranslation, Stream>(CorrectStream);
            Assert.AreEqual(OutTranslation.Id, actual.Id);
        }

        [Test]
        [Category("EntitiesFactory.Entities.Stream")]
        [Description("Creates empty translation from incorrect stream")]
        public void CreateEmptyTranslationIncorrectStreamTest()
        {
            CheckEmptyTranslation(SystemUnderTest.Create<ITranslation, Stream>(null));
        }

        #endregion

        #region EntitiesFactory.EF

        [Test]
        [Category("FactoryTests.Entities.EF")]
        [Description("Creates language from correct EF language")]
        public void CreateLanguageCorrectEfTest()
        {
            var actual = SystemUnderTest.Create<ILanguage, Language>(testLanguage);
            CheckLanguage(1, "english", "en", actual);
        }

        [Test]
        [Category("FactoryTests.Entities.EF")]
        [Description("Returns empty language if EF null")]
        public void CreateEmptyLangugeNullEfTest()
        {
            var actual = SystemUnderTest.Create<ILanguage, Language>(null);
            CheckLanguage(0, null, null, actual);
        }

        [Test]
        [Category("FactoryTests.Entities.EF")]
        [Description("Creates word from correct EF word")]
        public void CreateWordCorrectEfTest()
        {
            var actual = SystemUnderTest.Create<IWord, Word>(testWord);
            CheckWord(1, "spelling", "transcription", 1, "english", "en", actual);
        }

        [Test]
        [Category("FactoryTests.Entities.EF")]
        [Description("Creates empty word from null EF word")]
        public void CreateEmptyWordNullEfTest()
        {
            var actual = SystemUnderTest.Create<IWord, Word>(null);
            CheckWordEmptyLanguage(0, null, null, actual);
        }

        [Test]
        [Category("FactoryTests.Entities.EF")]
        [Description("Creates translation from correct EF translation")]
        public void CreateTranslationCorrectEfTest()
        {
            var actual = SystemUnderTest.Create<ITranslation, Translation>(testTranslation);
            Assert.AreEqual(1, actual.Id);
            CheckWord(1, "spelling", "transcription", 1, "english", "en", actual.Source);
            CheckWord(1, "spelling", "transcription", 1, "english", "en", actual.Target);
        }

        [Test]
        [Category("FactoryTests.Entities.EF")]
        [Description("Creates empty translation from null EF translation")]
        public void CreateEmptyTranslationNullEfTest()
        {
            var actual = SystemUnderTest.Create<ITranslation, Translation>(null);
            CheckEmptyTranslation(actual);
        }

        [Test]
        [Category("FactoryTests.Entities.EF")]
        [Description("Creates vocabbank from correct EF vocabbank")]
        public void CreateVocabBankCorrectTest()
        {
            var actual = SystemUnderTest.Create<IVocabBank, VocabBank>(testVocabBank);
            Assert.AreEqual(1, actual.Id);
            Assert.AreEqual("name", actual.Name);
            Assert.AreEqual("description", actual.Description);
            Assert.AreEqual(1, actual.Translations.Count);
            Assert.AreEqual(1, actual.Translations[0].Id);
            CheckWord(1, "spelling", "transcription", 1, "english", "en", actual.Translations[0].Source);
            CheckWord(1, "spelling", "transcription", 1, "english", "en", actual.Translations[0].Target);
        }

        [Test]
        [Category("FactoryTests.Entities.EF")]
        [Description("Creates empty vocabank from null EF vocabbank")]
        public void CreateVocabBankNullTest()
        {
            var actual = SystemUnderTest.Create<IVocabBank, VocabBank>(null);
            CheckEmptyVocabBank(actual);
        }

        [Test]
        [Category("FactoryTests.Entities.EF")]
        [Description("Creates tag from correct EF vocabbank")]
        public void CreateTagCorrectTest()
        {
            var actual = SystemUnderTest.Create<ITag, Tag>(testTag);
            CheckTag(1, "name", "description", actual);
        }

        [Test]
        [Category("FactoryTests.Entities.EF")]
        [Description("Creates tag from null EF tag")]
        public void CreateTagNullTest()
        {
            var actual = SystemUnderTest.Create<ITag, Tag>(null);
            CheckTag(0, null, null, actual);
        }

        #endregion

        #region EntitiesFactory.Empty
        [Test]
        [Category("FactoryTests.Entities.Empty")]
        [Description("Creates empty Language")]
        public void CreateEmptyLanguageTest()
        {
            var actual = SystemUnderTest.Create<ILanguage>();
            CheckLanguage(0, null, null, actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Empty")]
        [Description("Creates empty Word")]
        public void CreateEmptyWordTest()
        {
            var actual = SystemUnderTest.Create<IWord>();
            CheckWordEmptyLanguage(0, null, null, actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Empty")]
        [Description("Creates empty Translation")]
        public void CreateEmptyTranslationTest()
        {
            var actual = SystemUnderTest.Create<ITranslation>();
            CheckEmptyTranslation(actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Empty")]
        [Description("Creates empty vocabbank")]
        public void CreateEmptyVocabBankTest()
        {
            var actual = SystemUnderTest.Create<IVocabBank>();
            CheckEmptyVocabBank(actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Empty")]
        [Description("Creates empty tag")]
        public void CreateEmptyTag()
        {
            var actual = SystemUnderTest.Create<ITag>();
            CheckTag(0, null, null, actual);
        }

        #endregion
    }
}
