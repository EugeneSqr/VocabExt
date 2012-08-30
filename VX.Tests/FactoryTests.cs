using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.IO;
using System.Text;
using NUnit.Framework;
using VX.Domain.Entities;
using VX.Domain.Surrogates;
using VX.Model;
using VX.Service.Infrastructure.Factories;

namespace VX.Tests
{
    [TestFixture]
    internal class FactoryTests : TestsBase<IAbstractFactory, Factory>
    {
        private readonly IDictionary<string, Stream> summaryStreams = new Dictionary<string, Stream>
            {
                {"correct", GetStream("{\"Id\":1,\"Name\":\"Personality\",\"Description\":\"Vocabulary bank about personality\"}")},
                {"absentId", GetStream("{\"Name\":\"Personality\",\"Description\":\"Vocabulary bank about personality\"}")},
                {"nullId", GetStream("{\"Id\":null,\"Name\":\"Personality\",\"Description\":\"Vocabulary bank about personality\"}")},
                {"absentName", GetStream("{\"Id\":1,\"Description\":\"Vocabulary bank about personality\"}")},
                {"nullName", GetStream("{\"Id\":1,\"Name\":null,\"Description\":\"Vocabulary bank about personality\"}")},
                {"absentDescription", GetStream("{\"Id\":1,\"Name\":\"Personality\"}")},
                {"nullDescription", GetStream("{\"Id\":1,\"Name\":\"Personality\",\"Description\":null}")},
                {"incorrect", GetStream("{asdfasd-0as8d34985314@$&(*}")}
            };

        private readonly IDictionary<string, Stream> languageStreams = new Dictionary<string, Stream>
            {
                {"correct", GetStream("{\"Id\":1,\"Name\":\"english\",\"Abbreviation\":\"en\"}")},
                {"absentId", GetStream("{\"Name\":\"english\",\"Abbreviation\":\"en\"}")},
                {"nullId", GetStream("{\"Id\":null,\"Name\":\"english\",\"Abbreviation\":\"en\"}")},
                {"absentName", GetStream("{\"Id\":1,\"Abbreviation\":\"en\"}")},
                {"nullName", GetStream("{\"Id\":1,\"Name\":null,\"Abbreviation\":\"en\"}")},
                {"absentAbbreviation", GetStream("{\"Id\":1,\"Name\":\"english\"}")},
                {"nullAbbreviation", GetStream("{\"Id\":1,\"Name\":\"english\",null}")},
                {"incorrect", GetStream("{asdfasd-0as8d34985314@$&(*}")}
            };

        private readonly IDictionary<string, Stream> wordStreams = new Dictionary<string, Stream>
            {
                {"correct", GetStream("{\"Id\":1,\"Spelling\":\"TestSpelling\",\"Transcription\":\"TestTranscription\",\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Id\":1,\"Name\":\"english\",\"Abbreviation\":\"en\"}}")},
                {"absentId", GetStream("{\"Spelling\":\"TestSpelling\",\"Transcription\":\"TestTranscription\",\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Id\":1,\"Name\":\"english\",\"Abbreviation\":\"en\"}}")},
                {"nullId", GetStream("{\"Id\":null,\"Spelling\":\"TestSpelling\",\"Transcription\":\"TestTranscription\",\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Id\":1,\"Name\":\"english\",\"Abbreviation\":\"en\"}}")},
                {"absentSpelling", GetStream("{\"Id\":1,\"Transcription\":\"TestTranscription\",\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Id\":1,\"Name\":\"english\",\"Abbreviation\":\"en\"}}")},
                {"nullSpelling", GetStream("{\"Id\":1,\"Spelling\":null,\"Transcription\":\"TestTranscription\",\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Id\":1,\"Name\":\"english\",\"Abbreviation\":\"en\"}}")},
                {"absentTranscription", GetStream("{\"Id\":1,\"Spelling\":\"TestSpelling\",\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Id\":1,\"Name\":\"english\",\"Abbreviation\":\"en\"}}")},
                {"nullTranscription", GetStream("{\"Id\":1,\"Spelling\":\"TestSpelling\",\"Transcription\":null,\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Id\":1,\"Name\":\"english\",\"Abbreviation\":\"en\"}}")},
                {"absentLanguage", GetStream("{\"Id\":1,\"Spelling\":\"TestSpelling\",\"Transcription\":\"TestTranscription\"}")},
                {"nullLanguage", GetStream("{\"Id\":1,\"Spelling\":\"TestSpelling\",\"Transcription\":\"TestTranscription\",\"Language\":null}")},
                {"absentLanguageType", GetStream("{\"Id\":1,\"Spelling\":\"TestSpelling\",\"Transcription\":\"TestTranscription\",\"Language\":{\"Id\":1,\"Name\":\"english\",\"Abbreviation\":\"en\"}}")},
                {"nullLanguageType", GetStream("{\"Id\":1,\"Spelling\":\"TestSpelling\",\"Transcription\":\"TestTranscription\",\"Language\":{\"__type\":null,\"Id\":1,\"Name\":\"english\",\"Abbreviation\":\"en\"}}")},
                {"incorrectLanguageType", GetStream("{\"Id\":1,\"Spelling\":\"TestSpelling\",\"Transcription\":\"TestTranscription\",\"Language\":{\"__type\":\"LanguageContract:#VX.Domain111.Entities.Impl\",\"Id\":1,\"Name\":\"english\",\"Abbreviation\":\"en\"}}")},
                {"incorrect", GetStream("{asdfasd-0as8d34985314@$&(*}")}
            };

        private readonly IDictionary<string, Stream> translationStreams = new Dictionary<string, Stream>
            {
                {"correct", GetStream("{\"Id\":92,\"Source\":{\"__type\":\"WordContract:#VX.Domain.Entities.Impl\",\"Id\":1,\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Abbreviation\":\"en\",\"Id\":1,\"Name\":\"english\"},\"Spelling\":\"Spelling\",\"Transcription\":\"Transcription\"},\"Target\":{\"__type\":\"WordContract:#VX.Domain.Entities.Impl\",\"Id\":2,\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Abbreviation\":\"ru\",\"Id\":2,\"Name\":\"russian\"},\"Spelling\":\"Spelling2\",\"Transcription\":\"Transcription2\"}}")},
                {"absentId", GetStream("{\"Source\":{\"__type\":\"WordContract:#VX.Domain.Entities.Impl\",\"Id\":1,\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Abbreviation\":\"en\",\"Id\":1,\"Name\":\"english\"},\"Spelling\":\"Spelling\",\"Transcription\":\"Transcription\"},\"Target\":{\"__type\":\"WordContract:#VX.Domain.Entities.Impl\",\"Id\":2,\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Abbreviation\":\"ru\",\"Id\":2,\"Name\":\"russian\"},\"Spelling\":\"Spelling2\",\"Transcription\":\"Transcription2\"}}")},
                {"nullId", GetStream("{\"Id\":null,\"Source\":{\"__type\":\"WordContract:#VX.Domain.Entities.Impl\",\"Id\":1,\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Abbreviation\":\"en\",\"Id\":1,\"Name\":\"english\"},\"Spelling\":\"Spelling\",\"Transcription\":\"Transcription\"},\"Target\":{\"__type\":\"WordContract:#VX.Domain.Entities.Impl\",\"Id\":2,\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Abbreviation\":\"ru\",\"Id\":2,\"Name\":\"russian\"},\"Spelling\":\"Spelling2\",\"Transcription\":\"Transcription2\"}}")},
                {"absentSource", GetStream("{\"Id\":92,\"Target\":{\"__type\":\"WordContract:#VX.Domain.Entities.Impl\",\"Id\":2,\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Abbreviation\":\"ru\",\"Id\":2,\"Name\":\"russian\"},\"Spelling\":\"Spelling2\",\"Transcription\":\"Transcription2\"}}")},
                {"nullSource", GetStream("{\"Id\":92,\"Source\":null,\"Target\":{\"__type\":\"WordContract:#VX.Domain.Entities.Impl\",\"Id\":2,\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Abbreviation\":\"ru\",\"Id\":2,\"Name\":\"russian\"},\"Spelling\":\"Spelling2\",\"Transcription\":\"Transcription2\"}}")},
                {"absentTarget", GetStream("{\"Id\":92,\"Source\":{\"__type\":\"WordContract:#VX.Domain.Entities.Impl\",\"Id\":1,\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Abbreviation\":\"en\",\"Id\":1,\"Name\":\"english\"},\"Spelling\":\"Spelling\",\"Transcription\":\"Transcription\"}}")},
                {"nullTarget", GetStream("{\"Id\":92,\"Source\":{\"__type\":\"WordContract:#VX.Domain.Entities.Impl\",\"Id\":1,\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Abbreviation\":\"en\",\"Id\":1,\"Name\":\"english\"},\"Spelling\":\"Spelling\",\"Transcription\":\"Transcription\"},\"Target\":null}")},
                {"absentSourceWordType", GetStream("{\"Id\":92,\"Source\":{\"Id\":1,\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Abbreviation\":\"en\",\"Id\":1,\"Name\":\"english\"},\"Spelling\":\"Spelling\",\"Transcription\":\"Transcription\"},\"Target\":{\"__type\":\"WordContract:#VX.Domain.Entities.Impl\",\"Id\":2,\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Abbreviation\":\"ru\",\"Id\":2,\"Name\":\"russian\"},\"Spelling\":\"Spelling2\",\"Transcription\":\"Transcription2\"}}")},
                {"nullSourceWordType", GetStream("{\"Id\":92,\"Source\":{\"__type\":null,\"Id\":1,\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Abbreviation\":\"en\",\"Id\":1,\"Name\":\"english\"},\"Spelling\":\"Spelling\",\"Transcription\":\"Transcription\"},\"Target\":{\"__type\":\"WordContract:#VX.Domain.Entities.Impl\",\"Id\":2,\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Abbreviation\":\"ru\",\"Id\":2,\"Name\":\"russian\"},\"Spelling\":\"Spelling2\",\"Transcription\":\"Transcription2\"}}")},
                {"incorrectSourceWordType", GetStream("{\"Id\":92,\"Source\":{\"__type\":\"WordContract:#VX.Doma1212in.Entities.Impl\",\"Id\":1,\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Abbreviation\":\"en\",\"Id\":1,\"Name\":\"english\"},\"Spelling\":\"Spelling\",\"Transcription\":\"Transcription\"},\"Target\":{\"__type\":\"WordContract:#VX.Domain.Entities.Impl\",\"Id\":2,\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Abbreviation\":\"ru\",\"Id\":2,\"Name\":\"russian\"},\"Spelling\":\"Spelling2\",\"Transcription\":\"Transcription2\"}}")},
                {"absentTargetWordType", GetStream("{\"Id\":92,\"Source\":{\"__type\":\"WordContract:#VX.Domain.Entities.Impl\",\"Id\":1,\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Abbreviation\":\"en\",\"Id\":1,\"Name\":\"english\"},\"Spelling\":\"Spelling\",\"Transcription\":\"Transcription\"},\"Target\":{\"Id\":2,\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Abbreviation\":\"ru\",\"Id\":2,\"Name\":\"russian\"},\"Spelling\":\"Spelling2\",\"Transcription\":\"Transcription2\"}}")},
                {"nullTargetWordType", GetStream("{\"Id\":92,\"Source\":{\"__type\":\"WordContract:#VX.Domain.Entities.Impl\",\"Id\":1,\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Abbreviation\":\"en\",\"Id\":1,\"Name\":\"english\"},\"Spelling\":\"Spelling\",\"Transcription\":\"Transcription\"},\"Target\":{\"__type\":null,\"Id\":2,\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Abbreviation\":\"ru\",\"Id\":2,\"Name\":\"russian\"},\"Spelling\":\"Spelling2\",\"Transcription\":\"Transcription2\"}}")},
                {"incorrectTargetWordType", GetStream("{\"Id\":92,\"Source\":{\"__type\":\"WordContract:#VX.Domain.Entities.Impl\",\"Id\":1,\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Abbreviation\":\"en\",\"Id\":1,\"Name\":\"english\"},\"Spelling\":\"Spelling\",\"Transcription\":\"Transcription\"},\"Target\":{\"__type\":\"WordContract:#VX.Do21212main.Entities.Impl\",\"Id\":2,\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.Entities.Impl\",\"Abbreviation\":\"ru\",\"Id\":2,\"Name\":\"russian\"},\"Spelling\":\"Spelling2\",\"Transcription\":\"Transcription2\"}}")},
                {"incorrect", GetStream("{asdfasd-0as8d34985314@$&(*}")}
            };

        private readonly Language testLanguage;
        private readonly Word testWord;
        private readonly Translation testTranslation;
        private readonly VocabBank testVocabBank;
        private readonly Tag testTag;
        
        public FactoryTests()
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

        #region Entities.Stream

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Creates VocabBankSummary from correct stream")]
        public void CreateVocabBankSummaryCorrectTest()
        {
            var actual = SystemUnderTest.Create<IVocabBankSummary, Stream>(summaryStreams["correct"]);
            CheckVocabBankSummary(1, "Personality", "Vocabulary bank about personality", actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Creates empty VocabBankSummary from incorrect stream")]
        public void CreateVocaBankSummareIncorrectTest()
        {
            var actual = SystemUnderTest.Create<IVocabBankSummary, Stream>(summaryStreams["incorrect"]);
            CheckVocabBankSummary(0, null, null, actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Creates VocabBankSummary from incorrect id stream")]
        public void CreateVocabBankSummaryAbsentIdTest()
        {
            var actual = SystemUnderTest.Create<IVocabBankSummary, Stream>(summaryStreams["absentId"]);
            CheckVocabBankSummary(0, "Personality", "Vocabulary bank about personality", actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Creates empty VocabBankSummary from null id stream")]
        public void CreateVocabBankSummaryNullIdTest()
        {
            var actual = SystemUnderTest.Create<IVocabBankSummary, Stream>(summaryStreams["nullId"]);
            CheckVocabBankSummary(0, null, null, actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Creates VocabBankSummary from absent name stream")]
        public void CreateVocabBankSummaryAbsentNameTest()
        {
            var actual = SystemUnderTest.Create<IVocabBankSummary, Stream>(summaryStreams["absentName"]);
            CheckVocabBankSummary(1, null, "Vocabulary bank about personality", actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Creates VocabBankSummary from null name stream")]
        public void CreateVocabBankSummaryNullNameTest()
        {
            var actual = SystemUnderTest.Create<IVocabBankSummary, Stream>(summaryStreams["nullName"]);
            CheckVocabBankSummary(1, null, "Vocabulary bank about personality", actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Creates VocabBankSummary from absent description stream")]
        public void CreateVocabBankSummaryAbsentDescriptionTest()
        {
            var actual = SystemUnderTest.Create<IVocabBankSummary, Stream>(summaryStreams["absentDescription"]);
            CheckVocabBankSummary(1, "Personality", null, actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Creates VocabBankSummary from null description stream")]
        public void CreateVocabBankSummaryNullDescriptionTest()
        {
            var actual = SystemUnderTest.Create<IVocabBankSummary, Stream>(summaryStreams["nullDescription"]);
            CheckVocabBankSummary(1, "Personality", null, actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Creates Language from correct stream")]
        public void CreateLanguageCorrectTest()
        {
            var actual = SystemUnderTest.Create<ILanguage, Stream>(languageStreams["correct"]);
            CheckLanguage(1, "english", "en", actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Creates empty Language from incorrect stream")]
        public void CreateEmptyLanguageIncorrectTest()
        {
            var actual = SystemUnderTest.Create<ILanguage, Stream>(languageStreams["incorrect"]);
            CheckLanguage(0, null, null, actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Creates Language from absent id stream")]
        public void CreateLanguageAbsentIdTest()
        {
            var actual = SystemUnderTest.Create<ILanguage, Stream>(languageStreams["absentId"]);
            CheckLanguage(0, "english", "en", actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Creates Language from null id stream")]
        public void CreateEmptyLanguageNullIdTest()
        {
            var actual = SystemUnderTest.Create<ILanguage, Stream>(languageStreams["nullId"]);
            CheckLanguage(0, null, null, actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Creates Language from absent Name stream")]
        public void CreateLanguageAbsentNameTest()
        {
            var actual = SystemUnderTest.Create<ILanguage, Stream>(languageStreams["absentName"]);
            CheckLanguage(1, null, "en", actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Creates Language from null Name stream")]
        public void CreateEmptyLanguageNullNameTest()
        {
            var actual = SystemUnderTest.Create<ILanguage, Stream>(languageStreams["nullName"]);
            CheckLanguage(1, null, "en", actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Creates Word from correct stream")]
        public void CreateWordCorrectTest()
        {
            var actual = SystemUnderTest.Create<IWord, Stream>(wordStreams["correct"]);
            CheckWord(1, "TestSpelling", "TestTranscription", 1, "english", "en", actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Creates Word from incorrect stream")]
        public void CreateWordIncorrectTest()
        {
            var actual = SystemUnderTest.Create<IWord, Stream>(wordStreams["incorrect"]);
            CheckWordEmptyLanguage(0, null, null, actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Creates Word from absent id stream")]
        public void CreateWordAbsentIdTest()
        {
            var actual = SystemUnderTest.Create<IWord, Stream>(wordStreams["absentId"]);
            CheckWord(0, "TestSpelling", "TestTranscription", 1, "english", "en", actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Creates Word from null id stream")]
        public void CreateWordNullIdTest()
        {
            var actual = SystemUnderTest.Create<IWord, Stream>(wordStreams["nullId"]);
            CheckWordEmptyLanguage(0, null, null, actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Creates Word from absent spelling stream")]
        public void CreateWordAbsentSpellingTest()
        {
            var actual = SystemUnderTest.Create<IWord, Stream>(wordStreams["absentSpelling"]);
            CheckWord(1, null, "TestTranscription", 1, "english", "en", actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Creates Word from null spelling stream")]
        public void CreateWordNullSpellingTest()
        {
            var actual = SystemUnderTest.Create<IWord, Stream>(wordStreams["nullSpelling"]);
            CheckWord(1, null, "TestTranscription", 1, "english", "en", actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Creates Word from absent transcription stream")]
        public void CreateWordAbsentTranscriptionTest()
        {
            var actual = SystemUnderTest.Create<IWord, Stream>(wordStreams["absentTranscription"]);
            CheckWord(1, "TestSpelling", null, 1, "english", "en", actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Creates Word from null transcription stream")]
        public void CreateWordNullTranscriptionTest()
        {
            var actual = SystemUnderTest.Create<IWord, Stream>(wordStreams["nullTranscription"]);
            CheckWord(1, "TestSpelling", null, 1, "english", "en", actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Creates Word from absent language stream")]
        public void CreateWordAbsentLanguageTest()
        {
            var actual = SystemUnderTest.Create<IWord, Stream>(wordStreams["absentLanguage"]);
            CheckWordEmptyLanguage(1, "TestSpelling", "TestTranscription", actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Creates Word from null language stream")]
        public void CreateWordNullLanguageTest()
        {
            var actual = SystemUnderTest.Create<IWord, Stream>(wordStreams["nullLanguage"]);
            CheckWordEmptyLanguage(1, "TestSpelling", "TestTranscription", actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Creates Word from absent language type stream")]
        public void CreateWordAbsentLanguageTypeTest()
        {
            var actual = SystemUnderTest.Create<IWord, Stream>(wordStreams["absentLanguageType"]);
            CheckWordEmptyLanguage(0, null, null, actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Create Word from null language type stream")]
        public void CreateWordNullLanguageTypeTest()
        {
            var actual = SystemUnderTest.Create<IWord, Stream>(wordStreams["nullLanguageType"]);
            CheckWordEmptyLanguage(0, null, null, actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Create Word from incorrect language type stream")]
        public void CreateWordIncorrectLanguageTypeTest()
        {
            var actual = SystemUnderTest.Create<IWord, Stream>(wordStreams["incorrectLanguageType"]);
            CheckWordEmptyLanguage(0, null, null, actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Create Translation from correct stream")]
        public void CreateTranslationCorrectTest()
        {
            var actual = SystemUnderTest.Create<ITranslation, Stream>(translationStreams["correct"]);
            Assert.AreEqual(92, actual.Id);
            CheckWord(1, "Spelling", "Transcription", 1, "english", "en", actual.Source);
            CheckWord(2, "Spelling2", "Transcription2", 2, "russian", "ru", actual.Target);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Create empty Translation from incorrect stream")]
        public void CreateTranslationIncorrectTest()
        {
            var actual = SystemUnderTest.Create<ITranslation, Stream>(translationStreams["incorrect"]);
            CheckEmptyTranslation(actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Create Translation from absent id stream")]
        public void CreateTranslationAbsentId()
        {
            var actual = SystemUnderTest.Create<ITranslation, Stream>(translationStreams["absentId"]);
            Assert.AreEqual(0, actual.Id);
            CheckWord(1, "Spelling", "Transcription", 1, "english", "en", actual.Source);
            CheckWord(2, "Spelling2", "Transcription2", 2, "russian", "ru", actual.Target);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Create Translation from null id stream")]
        public void CreateTranslationNullIdTest()
        {
            var actual = SystemUnderTest.Create<ITranslation, Stream>(translationStreams["nullId"]);
            CheckEmptyTranslation(actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Create Translation from absent source stream")]
        public void CreateTranslationAbsentSourceTest()
        {
            var actual = SystemUnderTest.Create<ITranslation, Stream>(translationStreams["absentSource"]);
            Assert.AreEqual(92, actual.Id);
            Assert.IsNull(actual.Source);
            CheckWord(2, "Spelling2", "Transcription2", 2, "russian", "ru", actual.Target);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Create Translation from null source stream")]
        public void CreateTranslationNullSourceTest()
        {
            var actual = SystemUnderTest.Create<ITranslation, Stream>(translationStreams["nullSource"]);
            Assert.AreEqual(92, actual.Id);
            Assert.IsNull(actual.Source);
            CheckWord(2, "Spelling2", "Transcription2", 2, "russian", "ru", actual.Target);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Create Translation from absent target stream")]
        public void CreateTranslationAbsentTargetTest()
        {
            var actual = SystemUnderTest.Create<ITranslation, Stream>(translationStreams["absentTarget"]);
            Assert.AreEqual(92, actual.Id);
            CheckWord(1, "Spelling", "Transcription", 1, "english", "en", actual.Source);
            Assert.IsNull(actual.Target);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Create Translation from null target stream")]
        public void CreateTranslationNullTargetTest()
        {
            var actual = SystemUnderTest.Create<ITranslation, Stream>(translationStreams["nullTarget"]);
            Assert.AreEqual(92, actual.Id);
            CheckWord(1, "Spelling", "Transcription", 1, "english", "en", actual.Source);
            Assert.IsNull(actual.Target);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Create Translation from source word type absent stream")]
        public void CreateTranslationSourceWordTypeAbsentTest()
        {
            var actual = SystemUnderTest.Create<ITranslation, Stream>(translationStreams["absentSourceWordType"]);
            CheckEmptyTranslation(actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Create Translation from source word type null stream")]
        public void CreateTranslationSourceWordTypeNullTest()
        {
            var actual = SystemUnderTest.Create<ITranslation, Stream>(translationStreams["nullSourceWordType"]);
            CheckEmptyTranslation(actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Create Translation from source word type incorrect stream")]
        public void CreateTranslationSourceWordTypeIncorrectTest()
        {
            var actual = SystemUnderTest.Create<ITranslation, Stream>(translationStreams["incorrectSourceWordType"]);
            CheckEmptyTranslation(actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Create Translation from target word type absent stream")]
        public void CreateTranslationTargetWordTypeAbsentTest()
        {
            var actual = SystemUnderTest.Create<ITranslation, Stream>(translationStreams["absentTargetWordType"]);
            CheckEmptyTranslation(actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Create Translation from target word type null stream")]
        public void CreateTranslationTargetWordTypeNullTest()
        {
            var actual = SystemUnderTest.Create<ITranslation, Stream>(translationStreams["nullTargetWordType"]);
            CheckEmptyTranslation(actual);
        }

        [Test]
        [Category("FactoryTests.Entities.Stream")]
        [Description("Create Translation from target word type incorrect stream")]
        public void CreateTranslationTargetWordTypeIncorrectTest()
        {
            var actual = SystemUnderTest.Create<ITranslation, Stream>(translationStreams["incorrectTargetWordType"]);
            CheckEmptyTranslation(actual);
        }

        #endregion

        #region Entities.EF

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

        #region Entities.Empty
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
        [Description("Creates empty vocabbanksummary")]
        public void CreateEmptyVocabBankSummary()
        {
            var actual = SystemUnderTest.Create<IVocabBankSummary>();
            CheckVocabBankSummary(0, null, null, actual);
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

        #region ServiceOperationResponse

        [Test]
        [Category("FactoryTests.ServiceOperaitonResponse")]
        [Description("Creates new instance of ServiceOperationResponse with error message")]
        public void CreateErrorResponseTest()
        {
            var actual = SystemUnderTest.Create<IServiceOperationResponse>(false, ServiceOperationAction.None, "error");
            CheckResponse(false, null, "error", ServiceOperationAction.None, actual);
        }

        [Test]
        [Category("FactoryTests.ServiceOperaitonResponse")]
        [Description("Creates new instance of ServiceOperationResponse with status message")]
        public void CreateStatusResponseTest()
        {
            var actual = SystemUnderTest.Create<IServiceOperationResponse>(true, ServiceOperationAction.None, "status");
            CheckResponse(true, "status", null, ServiceOperationAction.None, actual);
        }

        #endregion

        private static void CheckLanguage(int expectedId, string expectedName, string expectedAbbreviation, ILanguage actual)
        {
            Assert.AreEqual(expectedId, actual.Id);
            Assert.AreEqual(expectedName, actual.Name);
            Assert.AreEqual(expectedAbbreviation, actual.Abbreviation);
        }

        private static void CheckWord(
            int expectedId, 
            string expectedSpelling, 
            string expectedTranscription, 
            int langId,
            string langName,
            string langAbbreviation,
            IWord actual)
        {
            Assert.AreEqual(expectedId, actual.Id);
            Assert.AreEqual(expectedSpelling, actual.Spelling);
            Assert.AreEqual(expectedTranscription, actual.Transcription);

            CheckLanguage(
                langId, 
                langName, 
                langAbbreviation, 
                actual.Language);
        }

        private static void CheckWordEmptyLanguage(
            int expectedId, 
            string expectedSpelling, 
            string expectedTranscription, 
            IWord actual)
        {
            Assert.AreEqual(expectedId, actual.Id);
            Assert.AreEqual(expectedSpelling, actual.Spelling);
            Assert.AreEqual(expectedTranscription, actual.Transcription);
            Assert.IsNull(actual.Language);
        }

        private static void CheckVocabBankSummary(
            int expectedId,
            string expectedName,
            string expectedDescription,
            IVocabBankSummary actual)
        {
            Assert.AreEqual(expectedId, actual.Id);
            Assert.AreEqual(expectedName, actual.Name);
            Assert.AreEqual(expectedDescription, actual.Description);
        }

        private static void CheckEmptyTranslation(ITranslation actual)
        {
            Assert.AreEqual(0, actual.Id);
            Assert.IsNull(actual.Source);
            Assert.IsNull(actual.Target);
        }

        private static void CheckResponse(
            bool expectedStatus, 
            string expectedStatusMessage, 
            string expectedErrorMessage, 
            ServiceOperationAction expectedAction, 
            IServiceOperationResponse actual)
        {
            Assert.AreEqual(expectedStatus, actual.Status);
            Assert.AreEqual(expectedStatusMessage, actual.StatusMessage);
            Assert.AreEqual((int)expectedAction, actual.OperationActionCode);
            Assert.AreEqual(expectedErrorMessage, actual.ErrorMessage);
        }

        private void CheckEmptyVocabBank(IVocabBank actual)
        {
            Assert.AreEqual(0, actual.Id);
            Assert.IsNull(actual.Name);
            Assert.IsNull(actual.Description);
            Assert.IsNull(actual.Translations);
            Assert.IsNull(actual.Tags);
        }

        private static void CheckTag(int expectedId, string expectedName, string expectedDescription, ITag actual)
        {
            Assert.AreEqual(expectedId, actual.Id);
            Assert.AreEqual(expectedName, actual.Name);
            Assert.AreEqual(expectedDescription, actual.Description);
        }

        private static MemoryStream GetStream(string source)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(source));
        }
    }
}
