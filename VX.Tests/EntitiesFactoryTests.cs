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
            CheckWord(
                1, 
                "spelling", 
                "transcription", 
                1, 
                "russian", 
                "ru", 
                SystemUnderTest.BuildWord(ConstructWord(1, "spelling", "transcription", ConstructLanguage(1, "russian", "ru"))));
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

        private static IDictionary<string, object> ConstructLanguage(int id, string name, string abbreviation)
        {
            var language = new Dictionary<string, object>();
            language["Id"] = id;
            language["Name"] = name;
            language["Abbreviation"] = abbreviation;
            return language;
        }

        private static IDictionary<string, object> ConstructWord(
            int id, 
            string spelling, 
            string transcription, 
            IDictionary<string, object> language)
        {
            var word = new Dictionary<string, object>();
            word["Id"] = id;
            word["Spelling"] = spelling;
            word["Transcription"] = transcription;
            word["Language"] = language;
            return word;
        }

        private static IDictionary<string, object> ConstructTranslation(
            int id, 
            IDictionary<string, object> source, 
            IDictionary<string, object> target)
        {
            var translation = new Dictionary<string, object>();
            translation["Id"] = id;
            translation["Source"] = source;
            translation["Target"] = target;
            return translation;
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

    }
}
