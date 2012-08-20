using System;
using System.Collections.Generic;
using NUnit.Framework;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Factories;
using VX.Service.Factories.Interfaces;

namespace VX.Tests
{
    [TestFixture]
    internal class EntitiesFactoryTests : TestsBase<IEntitiesFactory, EntitiesFactory>
    {
        public EntitiesFactoryTests()
        {
            BuildContainer();
        }

        [Test]
        [Category("EntitiesFactoryTests")]
        [Description("Checks if factory builds Language from json deserialized dictionary correctly")]
        public void BuildLanguageJsonDeserializedTest()
        {
            CheckLanguage(
                1, 
                "russian", 
                "ru", 
                SystemUnderTest.BuildLanguage(ConstructLanguage(1, "russian", "ru")));
        }

        [Test]
        [Category("EntitiesFactoryTests")]
        [Description("Checks if factory throws exception if input is null")]
        public void BuildLanguageJsonDeserializedExceptionTest()
        {
            Assert.Throws<NullReferenceException>(
                () => SystemUnderTest.BuildLanguage((IDictionary<string, object>) null));
        }

        [Test]
        [Category("EntitiesFactoryTests")]
        [Description("Checks if factory builds word if input is correct")]
        public void BuildWordJsonDeserializedTest()
        {
            CheckWord(1, "spelling", "transcription", 1, "russian", "ru", 
                SystemUnderTest.BuildWord(ConstructWord(1, "spelling", "transcription", ConstructLanguage(1, "russian", "ru"))));
        }

        [Test]
        [Category("EntitiesFactoryTests")]
        [Description("Checks if factory builds word with null transcription if transcription is absent")]
        public void BuildWordJsonDeserializedPartialTest()
        {
            CheckWord(1, "spelling", null, 1, "russian", "ru",
                      SystemUnderTest.BuildWord(ConstructWord(1, "spelling", null, ConstructLanguage(1, "russian", "ru"))));
        }

        [Test]
        [Category("EntitiesFactoryTests")]
        [Description("Checks if factory returns null if input id is null")]
        public void BuildWordJsonDeserializedPartialNullIdTest()
        {
            Assert.IsNull(
                SystemUnderTest.BuildWord(ConstructWord(null, "spelling", null, ConstructLanguage(1, "russian", "ru"))));
        }

        [Test]
        [Category("EntitiesFactoryTests")]
        [Description("Checks if factory returns null if input spelling is null")]
        public void BuildWordJsonDeserializedPartialNullSpellingTest()
        {
            Assert.IsNull(
                SystemUnderTest.BuildWord(ConstructWord(1, null, null, ConstructLanguage(1, "russian", "ru"))));
        }

        [Test]
        [Category("EntitiesFactoryTests")]
        [Description("Checks if factory returns null if input language is null")]
        public void BuildWordJsonDeserializedPartialNullLanguageTest()
        {
            Assert.IsNull(
                SystemUnderTest.BuildWord(ConstructWord(1, "spelling", null, null)));
        }

        [Test]
        [Category("EntitiesFactoryTests")]
        [Description("Checks if factory throws exception if input is null")]
        public void BuildWordJsonDeserializedExceptionTest()
        {
            Assert.Throws<NullReferenceException>(
                () => SystemUnderTest.BuildWord((IDictionary<string, object>) null));
        }

        [Test]
        [Category("EntitiesFactoryTests")]
        [Description("Checks if factory builds translation if input is correct")]
        public void BuildTranslationJsonDeserializedTest()
        {
            var actual = SystemUnderTest.BuildTranslation(ConstructTranslation(
                1,
                ConstructWord(1, "wordspelling1", "wordtranscription1", ConstructLanguage(1, "russian", "ru")), 
                ConstructWord(2, "wordspelling2", "wordtranscription2", ConstructLanguage(2, "english", "en"))));

            Assert.AreEqual(1, actual.Id);
            CheckWord(1, "wordspelling1", "wordtranscription1", 1, "russian", "ru", actual.Source);
            CheckWord(2, "wordspelling2", "wordtranscription2", 2, "english", "en", actual.Target);
        }

        [Test]
        [Category("EntitiesFactoryTests")]
        [Description("Checks if factory throws exception if input is null")]
        public void BuildTranslationJsonDeserializedExceptionTest()
        {
            Assert.Throws<NullReferenceException>(
                () => SystemUnderTest.BuildTranslation((IDictionary<string, object>)null));
        }

        [Test]
        [Category("EntitiesFactoryTests")]
        [Description("Checks if factory builds ManyToManyRelationship object correctly")]
        public void BuildManyToManyRelationshipTest()
        {
            var actual = SystemUnderTest.BuildManyToManyRelationship(1, 2, 3);
            Assert.AreEqual(1, actual.Id);
            Assert.AreEqual(2, actual.SourceId);
            Assert.AreEqual(3, actual.TargetId);
        }

        [Test]
        [Category("EntitiesFactoryTests")]
        [Description("Checks if factory builds vocab bank from deserialized json string")]
        public void BuildVocabBankHeadersJsonDeserializedTest()
        {
            var actual = SystemUnderTest.BuildVocabBankHeaders(
                ConstructVocabBankHeaders(1, "name", "description"));
            Assert.AreEqual(1, actual.Id);
            Assert.AreEqual("name", actual.Name);
            Assert.AreEqual("description", actual.Description);
        }

        private static IDictionary<string, object> ConstructLanguage(int id, string name, string abbreviation)
        {
            var language = new Dictionary<string, object>();
            InsertIntoDictionary(language, "Id", id);
            InsertIntoDictionary(language, "Name", name);
            InsertIntoDictionary(language, "Abbreviation", abbreviation);
            return language;
        }

        private static IDictionary<string, object> ConstructWord(
            int? id, 
            string spelling, 
            string transcription, 
            IDictionary<string, object> language)
        {
            var word = new Dictionary<string, object>();
            InsertIntoDictionary(word, "Id", id);
            InsertIntoDictionary(word, "Spelling", spelling);
            InsertIntoDictionary(word, "Transcription", transcription);
            InsertIntoDictionary(word, "Language", language);
            return word;
        }

        private static IDictionary<string, object> ConstructTranslation(
            int id, 
            IDictionary<string, object> source, 
            IDictionary<string, object> target)
        {
            var translation = new Dictionary<string, object>();
            InsertIntoDictionary(translation, "Id", id);
            InsertIntoDictionary(translation, "Source", source);
            InsertIntoDictionary(translation, "Target", target);
            return translation;
        }

        private static IDictionary<string, object> ConstructVocabBankHeaders(
            int id, 
            string name, 
            string description)
        {
            var vocabBank = new Dictionary<string, object>();
            InsertIntoDictionary(vocabBank, "VocabBankId", id);
            InsertIntoDictionary(vocabBank, "Name", name);
            InsertIntoDictionary(vocabBank, "Description", description);
            return vocabBank;
        }

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

        private static void InsertIntoDictionary(IDictionary<string, object> dictionary, string key, object value)
        {
            if (value != null)
            {
                dictionary[key] = value;
            }
        }
    }
}
