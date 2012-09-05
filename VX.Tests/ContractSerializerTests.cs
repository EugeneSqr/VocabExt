using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using VX.Domain.Entities;
using VX.Domain.Entities.Impl;
using VX.Domain.Surrogates;
using VX.Domain.Surrogates.Impl;
using VX.Service.Infrastructure;
using VX.Service.Infrastructure.Interfaces;
using VX.Tests.FactoriesTests;

namespace VX.Tests
{
    [TestFixture]
    public class ContractSerializerTests : DomainItemsCheckTests<IContractSerializer, ContractSerializer>
    {
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

        private readonly IDictionary<string, Stream> bankTranslationPairStreams = new Dictionary<string, Stream>
            {
                {"correct", GetStream("{\"Translation\":{\"__type\":\"TranslationContract:#VX.Domain.Entities.Impl\",\"Id\":1,\"Source\":null,\"Target\":null},\"VocabBankId\":1}")},
                {"absentId", GetStream("{\"Translation\":{\"__type\":\"TranslationContract:#VX.Domain.Entities.Impl\",\"Id\":1,\"Source\":null,\"Target\":null}}")},
                {"nullId", GetStream("{\"Translation\":{\"__type\":\"TranslationContract:#VX.Domain.Entities.Impl\",\"Id\":1,\"Source\":null,\"Target\":null},\"VocabBankId\":null}")},
                {"absentTranslation", GetStream("{\"VocabBankId\":1}")},
                {"nullTranslation", GetStream("{\"Translation\":null,\"VocabBankId\":1}")},
                {"absentTranslationType", GetStream("{\"Translation\":{\"Id\":1,\"Source\":null,\"Target\":null},\"VocabBankId\":1}")},
                {"nullTranslationType", GetStream("{\"Translation\":{\"__type\":null,\"Id\":1,\"Source\":null,\"Target\":null},\"VocabBankId\":1}")},
                {"incorrectTranslationType", GetStream("{\"Translation\":{\"__type\":\"Transl1212ationContract:#VX.Domain.Entities.Impl\",\"Id\":1,\"Source\":null,\"Target\":null},\"VocabBankId\":1}")},
                {"incorrect", GetStream("{asdfasd-0as8d34985314@$&(*}")}
            };

        private readonly IDictionary<string, Stream> parentChildIdPairStreams = new Dictionary<string, Stream>
            {
                {"correct", GetStream("{\"ParentId\":1,\"ChildId\":1}")},
                {"absentParentId", GetStream("{\"ChildId\":1}")},
                {"nullParentId", GetStream("{\"ParentId\":null,\"ChildId\":1}")},
                {"absentChildId", GetStream("{\"ParentId\":1}")},
                {"nullChildId", GetStream("{\"ParentId\":1,\"ChildId\":null}")},
                {"incorrect", GetStream("{asdfasd-0as8d34985314@$&(*}")}
            };
        
        public ContractSerializerTests()
        {
            BuildContainer();
        }

        #region Entities
        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Creates Language from correct stream")]
        public void CreateLanguageCorrectTest()
        {
            ILanguage actual;
            Assert.IsTrue(SystemUnderTest.Deserialize<ILanguage, LanguageContract>(languageStreams["correct"], out actual));
            CheckLanguage(1, "english", "en", actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Creates empty Language from incorrect stream")]
        public void CreateEmptyLanguageIncorrectTest()
        {
            ILanguage actual;
            Assert.IsFalse(SystemUnderTest.Deserialize<ILanguage, LanguageContract>(languageStreams["incorrect"], out actual));
            Assert.IsNull(actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Creates Language from absent id stream")]
        public void CreateLanguageAbsentIdTest()
        {
            ILanguage actual;
            Assert.IsTrue(SystemUnderTest.Deserialize<ILanguage, LanguageContract>(languageStreams["absentId"], out actual));
            CheckLanguage(0, "english", "en", actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Creates Language from null id stream")]
        public void CreateEmptyLanguageNullIdTest()
        {
            ILanguage actual;
            Assert.IsFalse(SystemUnderTest.Deserialize<ILanguage, LanguageContract>(languageStreams["nullId"], out actual));
            Assert.IsNull(actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Creates Language from absent Name stream")]
        public void CreateLanguageAbsentNameTest()
        {
            ILanguage actual;
            Assert.IsTrue(SystemUnderTest.Deserialize<ILanguage, LanguageContract>(languageStreams["absentName"], out actual));
            CheckLanguage(1, null, "en", actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Creates Language from null Name stream")]
        public void CreateEmptyLanguageNullNameTest()
        {
            ILanguage actual;
            Assert.IsTrue(SystemUnderTest.Deserialize<ILanguage, LanguageContract>(languageStreams["nullName"], out actual));
            CheckLanguage(1, null, "en", actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Creates Word from correct stream")]
        public void CreateWordCorrectTest()
        {
            IWord actual;
            Assert.IsTrue(SystemUnderTest.Deserialize<IWord, WordContract>(wordStreams["correct"], out actual));
            CheckWord(1, "TestSpelling", "TestTranscription", 1, "english", "en", actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Creates empty Word from incorrect stream")]
        public void CreateWordIncorrectTest()
        {
            IWord actual;
            Assert.IsFalse(SystemUnderTest.Deserialize<IWord, WordContract>(wordStreams["incorrect"], out actual));
            Assert.IsNull(actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Creates Word from absent id stream")]
        public void CreateWordAbsentIdTest()
        {
            IWord actual;
            Assert.IsTrue(SystemUnderTest.Deserialize<IWord, WordContract>(wordStreams["absentId"], out actual));
            CheckWord(0, "TestSpelling", "TestTranscription", 1, "english", "en", actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Creates empty Word from null id stream")]
        public void CreateWordNullIdTest()
        {
            IWord actual;
            Assert.IsFalse(SystemUnderTest.Deserialize<IWord, WordContract>(wordStreams["nullId"], out actual));
            Assert.IsNull(actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Creates Word from absent spelling stream")]
        public void CreateWordAbsentSpellingTest()
        {
            IWord actual;
            Assert.IsTrue(SystemUnderTest.Deserialize<IWord, WordContract>(wordStreams["absentSpelling"], out actual));
            CheckWord(1, null, "TestTranscription", 1, "english", "en", actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Creates Word from null spelling stream")]
        public void CreateWordNullSpellingTest()
        {
            IWord actual;
            Assert.IsTrue(SystemUnderTest.Deserialize<IWord, WordContract>(wordStreams["nullSpelling"], out actual));
            CheckWord(1, null, "TestTranscription", 1, "english", "en", actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Creates Word from absent transcription stream")]
        public void CreateWordAbsentTranscriptionTest()
        {
            IWord actual;
            Assert.IsTrue(SystemUnderTest.Deserialize<IWord, WordContract>(wordStreams["absentTranscription"], out actual));
            CheckWord(1, "TestSpelling", null, 1, "english", "en", actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Creates Word from null transcription stream")]
        public void CreateWordNullTranscriptionTest()
        {
            IWord actual;
            Assert.IsTrue(SystemUnderTest.Deserialize<IWord, WordContract>(wordStreams["nullTranscription"], out actual));
            CheckWord(1, "TestSpelling", null, 1, "english", "en", actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Creates Word from absent language stream")]
        public void CreateWordAbsentLanguageTest()
        {
            IWord actual;
            Assert.IsTrue(SystemUnderTest.Deserialize<IWord, WordContract>(wordStreams["absentLanguage"], out actual));
            CheckWordEmptyLanguage(1, "TestSpelling", "TestTranscription", actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Creates Word from null language stream")]
        public void CreateWordNullLanguageTest()
        {
            IWord actual;
            Assert.IsTrue(SystemUnderTest.Deserialize<IWord, WordContract>(wordStreams["nullLanguage"], out actual));
            CheckWordEmptyLanguage(1, "TestSpelling", "TestTranscription", actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Creates empty Word from absent language type stream")]
        public void CreateWordAbsentLanguageTypeTest()
        {
            IWord actual;
            Assert.IsFalse(SystemUnderTest.Deserialize<IWord, WordContract>(wordStreams["absentLanguageType"], out actual));
            Assert.IsNull(actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Create empty Word from null language type stream")]
        public void CreateWordNullLanguageTypeTest()
        {
            IWord actual;
            Assert.IsFalse(SystemUnderTest.Deserialize<IWord, WordContract>(wordStreams["nullLanguageType"], out actual));
            Assert.IsNull(actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Create empty Word from incorrect language type stream")]
        public void CreateWordIncorrectLanguageTypeTest()
        {
            IWord actual;
            Assert.IsFalse(SystemUnderTest.Deserialize<IWord, WordContract>(wordStreams["incorrectLanguageType"], out actual));
            Assert.IsNull(actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Create Translation from correct stream")]
        public void CreateTranslationCorrectTest()
        {
            ITranslation actual;
            Assert.IsTrue(SystemUnderTest.Deserialize<ITranslation, TranslationContract>(translationStreams["correct"], out actual));
            Assert.AreEqual(92, actual.Id);
            CheckWord(1, "Spelling", "Transcription", 1, "english", "en", actual.Source);
            CheckWord(2, "Spelling2", "Transcription2", 2, "russian", "ru", actual.Target);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Create empty Translation from incorrect stream")]
        public void CreateTranslationIncorrectTest()
        {
            ITranslation actual;
            Assert.IsFalse(SystemUnderTest.Deserialize<ITranslation, TranslationContract>(translationStreams["incorrect"], out actual));
            Assert.IsNull(actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Create Translation from absent id stream")]
        public void CreateTranslationAbsentId()
        {
            ITranslation actual;
            Assert.IsTrue(SystemUnderTest.Deserialize<ITranslation, TranslationContract>(translationStreams["absentId"], out actual));
            Assert.AreEqual(0, actual.Id);
            CheckWord(1, "Spelling", "Transcription", 1, "english", "en", actual.Source);
            CheckWord(2, "Spelling2", "Transcription2", 2, "russian", "ru", actual.Target);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Create empty Translation from null id stream")]
        public void CreateTranslationNullIdTest()
        {
            ITranslation actual;
            Assert.IsFalse(SystemUnderTest.Deserialize<ITranslation, TranslationContract>(translationStreams["nullId"], out actual));
            Assert.IsNull(actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Create Translation from absent source stream")]
        public void CreateTranslationAbsentSourceTest()
        {
            ITranslation actual;
            Assert.IsTrue(SystemUnderTest.Deserialize<ITranslation, TranslationContract>(translationStreams["absentSource"], out actual));
            Assert.AreEqual(92, actual.Id);
            Assert.IsNull(actual.Source);
            CheckWord(2, "Spelling2", "Transcription2", 2, "russian", "ru", actual.Target);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Create Translation from null source stream")]
        public void CreateTranslationNullSourceTest()
        {
            ITranslation actual;
            Assert.IsTrue(SystemUnderTest.Deserialize<ITranslation, TranslationContract>(translationStreams["nullSource"], out actual));
            Assert.AreEqual(92, actual.Id);
            Assert.IsNull(actual.Source);
            CheckWord(2, "Spelling2", "Transcription2", 2, "russian", "ru", actual.Target);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Create Translation from absent target stream")]
        public void CreateTranslationAbsentTargetTest()
        {
            ITranslation actual;
            Assert.IsTrue(SystemUnderTest.Deserialize<ITranslation, TranslationContract>(translationStreams["absentTarget"], out actual));
            Assert.AreEqual(92, actual.Id);
            CheckWord(1, "Spelling", "Transcription", 1, "english", "en", actual.Source);
            Assert.IsNull(actual.Target);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Create Translation from null target stream")]
        public void CreateTranslationNullTargetTest()
        {
            ITranslation actual;
            Assert.IsTrue(SystemUnderTest.Deserialize<ITranslation, TranslationContract>(translationStreams["nullTarget"], out actual));
            Assert.AreEqual(92, actual.Id);
            CheckWord(1, "Spelling", "Transcription", 1, "english", "en", actual.Source);
            Assert.IsNull(actual.Target);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Create empty Translation from source word type absent stream")]
        public void CreateTranslationSourceWordTypeAbsentTest()
        {
            ITranslation actual;
            Assert.IsFalse(SystemUnderTest.Deserialize<ITranslation, TranslationContract>(translationStreams["absentSourceWordType"], out actual));
            Assert.IsNull(actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Create empty Translation from source word type null stream")]
        public void CreateTranslationSourceWordTypeNullTest()
        {
            ITranslation actual;
            Assert.IsFalse(SystemUnderTest.Deserialize<ITranslation, TranslationContract>(translationStreams["nullSourceWordType"], out actual));
            Assert.IsNull(actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Create empty Translation from source word type incorrect stream")]
        public void CreateTranslationSourceWordTypeIncorrectTest()
        {
            ITranslation actual;
            Assert.IsFalse(SystemUnderTest.Deserialize<ITranslation, TranslationContract>(translationStreams["incorrectSourceWordType"], out actual));
            Assert.IsNull(actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Create empty Translation from target word type absent stream")]
        public void CreateTranslationTargetWordTypeAbsentTest()
        {
            ITranslation actual;
            Assert.IsFalse(SystemUnderTest.Deserialize<ITranslation, TranslationContract>(translationStreams["absentTargetWordType"], out actual));
            Assert.IsNull(actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Create empty Translation from target word type null stream")]
        public void CreateTranslationTargetWordTypeNullTest()
        {
            ITranslation actual;
            Assert.IsFalse(SystemUnderTest.Deserialize<ITranslation, TranslationContract>(translationStreams["nullTargetWordType"], out actual));
            Assert.IsNull(actual);
        }

        [Test]
        [Category("ContractSerializerTests.Entities")]
        [Description("Create empty Translation from target word type incorrect stream")]
        public void CreateTranslationTargetWordTypeIncorrectTest()
        {
            ITranslation actual;
            Assert.IsFalse(SystemUnderTest.Deserialize<ITranslation, TranslationContract>(translationStreams["incorrectTargetWordType"], out actual));
            Assert.IsNull(actual);
        }
        #endregion

        #region Surrogates

        [Test]
        [Category("ContractSerializerTests.Surrogates")]
        [Description("Creates VocabBankSummary from correct stream")]
        public void CreateVocabBankSummaryCorrectTest()
        {
            IVocabBankSummary actual;
            Assert.IsTrue(
                SystemUnderTest.Deserialize<IVocabBankSummary, VocabBankSummary>(summaryStreams["correct"],out actual));
            CheckVocabBankSummary(1, "Personality", "Vocabulary bank about personality", actual);
        }

        [Test]
        [Category("ContractSerializerTests.Surrogates")]
        [Description("Creates empty VocabBankSummary from incorrect stream")]
        public void CreateVocaBankSummareIncorrectTest()
        {
            IVocabBankSummary actual;
            Assert.IsFalse(
                SystemUnderTest.Deserialize<IVocabBankSummary, VocabBankSummary>(summaryStreams["incorrect"], out actual));
            Assert.IsNull(actual);
        }

        [Test]
        [Category("ContractSerializerTests.Surrogates")]
        [Description("Creates VocabBankSummary from incorrect id stream")]
        public void CreateVocabBankSummaryAbsentIdTest()
        {
            IVocabBankSummary actual;
            Assert.IsTrue(
                SystemUnderTest.Deserialize<IVocabBankSummary, VocabBankSummary>(summaryStreams["absentId"], out actual));
            CheckVocabBankSummary(0, "Personality", "Vocabulary bank about personality", actual);
        }

        [Test]
        [Category("ContractSerializerTests.Surrogates")]
        [Description("Creates empty VocabBankSummary from null id stream")]
        public void CreateVocabBankSummaryNullIdTest()
        {
            IVocabBankSummary actual;
            Assert.IsFalse(
                SystemUnderTest.Deserialize<IVocabBankSummary, VocabBankSummary>(summaryStreams["nullId"], out actual));
            Assert.IsNull(actual);
        }

        [Test]
        [Category("ContractSerializerTests.Surrogates")]
        [Description("Creates VocabBankSummary from absent name stream")]
        public void CreateVocabBankSummaryAbsentNameTest()
        {
            IVocabBankSummary actual;
            Assert.IsTrue(
                SystemUnderTest.Deserialize<IVocabBankSummary, VocabBankSummary>(summaryStreams["absentName"], out actual));
            CheckVocabBankSummary(1, null, "Vocabulary bank about personality", actual);
        }

        [Test]
        [Category("ContractSerializerTests.Surrogates")]
        [Description("Creates VocabBankSummary from null name stream")]
        public void CreateVocabBankSummaryNullNameTest()
        {
            IVocabBankSummary actual;
            Assert.IsTrue(
                SystemUnderTest.Deserialize<IVocabBankSummary, VocabBankSummary>(summaryStreams["nullName"], out actual));
            CheckVocabBankSummary(1, null, "Vocabulary bank about personality", actual);
        }

        [Test]
        [Category("ContractSerializerTests.Surrogates")]
        [Description("Creates VocabBankSummary from absent description stream")]
        public void CreateVocabBankSummaryAbsentDescriptionTest()
        {
            IVocabBankSummary actual;
            Assert.IsTrue(
                SystemUnderTest.Deserialize<IVocabBankSummary, VocabBankSummary>(
                    summaryStreams["absentDescription"], out actual));
            CheckVocabBankSummary(1, "Personality", null, actual);
        }

        [Test]
        [Category("ContractSerializerTests.Surrogates")]
        [Description("Creates VocabBankSummary from null description stream")]
        public void CreateVocabBankSummaryNullDescriptionTest()
        {
            IVocabBankSummary actual;
            Assert.IsTrue(
                SystemUnderTest.Deserialize<IVocabBankSummary, VocabBankSummary>(
                    summaryStreams["nullDescription"], out actual));
            CheckVocabBankSummary(1, "Personality", null, actual);
        }

        [Test]
        [Category("ContractSerializerTests.Surrogates")]
        [Description("Creates BankTranslationPair from correct stream")]
        public void CreateBankTranslationPairCorrectTest()
        {
            IBankTranslationPair actual;
            Assert.IsTrue(CallBankTranslationPairDeserialize("correct", out actual));
            CheckBankTranslationPair(1, 1, actual);
        }

        [Test]
        [Category("ContractSerializerTests.Surrogates")]
        [Description("Creates empty BankTranslationpair from incorrect stream")]
        public void CreateEmptyBankTranslationPairIncorrectTest()
        {
            IBankTranslationPair actual;
            Assert.IsFalse(CallBankTranslationPairDeserialize("incorrect", out actual));
            Assert.IsNull(actual);
        }

        [Test]
        [Category("ContractSerializerTests.Surrogates")]
        [Description("Creates BankTranslationPair from absent id stream")]
        public void CreateBankTranslationPairAbsentIdTest()
        {
            IBankTranslationPair actual;
            Assert.IsTrue(CallBankTranslationPairDeserialize("absentId", out actual));
            CheckBankTranslationPair(0, 1, actual);
        }

        [Test]
        [Category("ContractSerializerTests.Surrogates")]
        [Description("Creates empty BankTranslationPair from null id stream")]
        public void CreateBankTranslationPairNullIdTest()
        {
            IBankTranslationPair actual;
            Assert.IsFalse(CallBankTranslationPairDeserialize("nullId", out actual));
            Assert.IsNull(actual);
        }

        [Test]
        [Category("ContractSerializerTests.Surrogates")]
        [Description("Creates BankTranslationPair from absent translation stream")]
        public void CreateBankTranslationPairAbsentTranslationTest()
        {
            IBankTranslationPair actual;
            Assert.IsTrue(CallBankTranslationPairDeserialize("absentTranslation", out actual));
            CheckBankTranslationPairEmptyTranslation(1, actual);
        }

        [Test]
        [Category("ContractSerializerTests.Surrogates")]
        [Description("Creates BankTranslationPair from null translation stream")]
        public void CreateBankTranslationPairNullTranslationTest()
        {
            IBankTranslationPair actual;
            Assert.IsTrue(CallBankTranslationPairDeserialize("nullTranslation", out actual));
            CheckBankTranslationPairEmptyTranslation(1, actual);
        }

        [Test]
        [Category("ContractSerializerTests.Surrogates")]
        [Description("Creates empty BankTranslationPair from absent translation type stream")]
        public void CreateBankTranslationPairAbsentTranslationTypeTest()
        {
            IBankTranslationPair actual;
            Assert.IsFalse(CallBankTranslationPairDeserialize("absentTranslationType", out actual));
            Assert.IsNull(actual);
        }

        [Test]
        [Category("ContractSerializerTests.Surrogates")]
        [Description("Creates empty BankTranslationPair from null translation type stream")]
        public void CreateBankTranslationPairNullTranslationTypeTest()
        {
            IBankTranslationPair actual;
            Assert.IsFalse(CallBankTranslationPairDeserialize("nullTranslationType", out actual));
            Assert.IsNull(actual);
        }

        [Test]
        [Category("ContractSerializerTests.Surrogates")]
        [Description("Creates empty BankTranslationPair from incorrect translation type stream")]
        public void CreateBankTranslationPairIncorrectTranslationTypeTest()
        {
            IBankTranslationPair actual;
            Assert.IsFalse(CallBankTranslationPairDeserialize("incorrectTranslationType", out actual));
            Assert.IsNull(actual);
        }

        [Test]
        [Category("ContractSerializerTests.Surrogates")]
        [Description("Creates ParentChildIdPair from correct stream")]
        public void CreateParentChildIdPairCorrectTest()
        {
            IParentChildIdPair actual;
            Assert.IsTrue(CallParentChildIdPairDeserialize("correct", out actual));
            CheckParentChildIdPair(1, 1, actual);
        }

        [Test]
        [Category("ContractSerializerTests.Surrogates")]
        [Description("Creates ParentChildIdPair from incorrect stream")]
        public void CreateParentChildIdPairIncorrectTest()
        {
            IParentChildIdPair actual;
            Assert.IsFalse(CallParentChildIdPairDeserialize("incorrect", out actual));
            Assert.IsNull(actual);
        }

        [Test]
        [Category("ContractSerializerTests.Surrogates")]
        [Description("Creates ParentChildIdPair from absent parent id stream")]
        public void CreateParentChildIdPairAbsentParentIdTest()
        {
            IParentChildIdPair actual;
            Assert.IsTrue(CallParentChildIdPairDeserialize("absentParentId", out actual));
            CheckParentChildIdPair(0, 1, actual);
        }

        [Test]
        [Category("ContractSerializerTests.Surrogates")]
        [Description("Creates empty ParentChildIdPair from null parent id stream")]
        public void CreateEmptyParentChildIdPairNullParentIdTest()
        {
            IParentChildIdPair actual;
            Assert.IsFalse(CallParentChildIdPairDeserialize("nullParentId", out actual));
            Assert.IsNull(actual);
        }

        [Test]
        [Category("ContractSerializerTests.Surrogates")]
        [Description("Creates ParentChildIdPair from absent child id stream")]
        public void CreateParentChildIdPairAbsentChildIdTest()
        {
            IParentChildIdPair actual;
            Assert.IsTrue(CallParentChildIdPairDeserialize("absentChildId", out actual));
            CheckParentChildIdPair(1, 0, actual);
        }

        [Test]
        [Category("ContractSerializerTests.Surrogates")]
        [Description("Creates empty ParentChildIdPair from null child id stream")]
        public void CreateEmptyParentChildIdPairNullChildIdTest()
        {
            IParentChildIdPair actual;
            Assert.IsFalse(CallParentChildIdPairDeserialize("nullChildId", out actual));
            Assert.IsNull(actual);
        }

        #endregion

        private bool CallBankTranslationPairDeserialize(string streamKey, out IBankTranslationPair actual)
        {
            return SystemUnderTest.Deserialize<IBankTranslationPair, BankTranslationPair>(
                bankTranslationPairStreams[streamKey], out actual);
        }

        private bool CallParentChildIdPairDeserialize(string streamKey, out IParentChildIdPair actual)
        {
            return SystemUnderTest.Deserialize<IParentChildIdPair, ParentChildIdPair>(
                parentChildIdPairStreams[streamKey], out actual);
        }
    }
}
