using System.IO;
using System.Text;
using Autofac;
using NUnit.Framework;
using VX.Service.Factories.Interfaces;
using VX.Service.Infrastructure;
using VX.Service.Infrastructure.Interfaces;
using VX.Tests.Mocks;

namespace VX.Tests
{
    [TestFixture]
    public class InputDataConverterTests : TestsBase<IInputDataConverter, InputDataConverter>
    {
        private const string IncorrectJsonString = "\asdf%as$d!k@h]";

        private const string TestBankTranslationPairCorrectJsonString = 
            "{\"VocabBankId\":1,\"Translation\":{\"Id\":11,\"Source\":{\"Id\":17,\"Language\":{\"Id\":1,\"Name\":\"english\",\"Abbreviation\":\"en\"},\"Spelling\":\"conscientious\",\"Transcription\":\"?k?n(t)??'en(t)??s\"},\"Target\":{\"Id\":18,\"Language\":{\"Id\":2,\"Name\":\"russian\",\"Abbreviation\":\"ru\"},\"Spelling\":\"добросовестный\",\"Transcription\":\"дабрас`ов'издный'\"}}}";
        
        private const string TestBankHeadersCorrectJsonString =
            "{\"VocabBankId\":1,\"Name\":\"Personality\",\"Description\":\"Vocabulary bank about personality\"}";

        private const string TestParsePairCorrectJsonString = "{\"parent\":\"1\", \"child\": 2}";
            
        
        public InputDataConverterTests()
        {
            ContainerBuilder.RegisterInstance(new JavaScriptConvertersFactoryMock())
                .As<IJavaScriptConvertersFactory>()
                .SingleInstance();
            
            BuildContainer();
        }

        [Test]
        [Category("InputDataConverterTests")]
        [Description("Checks if Convert parses string to int correctly")]
        public void ConvertStringToIntTest()
        {
            Assert.AreEqual(1, SystemUnderTest.Convert("1")); 
        }

        [Test]
        [Category("InputDataConverterTests")]
        [Description("Checks if Convert returns empty int if convertion is impossible")]
        public void ConvertStringToIntEmptyTest()
        {
            Assert.AreEqual(SystemUnderTest.EmptyId, SystemUnderTest.Convert("asd"));
        }

        [Test]
        [Category("InputDataConverterTests")]
        [Description("Checks if ParsePair parses input json string with key - value pair inside")]
        public void ParsePairJsonToDictionaryTest()
        {
            var inputStream = GetStreamFromString(TestParsePairCorrectJsonString);
            var actual = SystemUnderTest.ParsePair(inputStream);
            Assert.AreEqual(1, actual.ParentId);
            Assert.AreEqual(2, actual.ChildId);
        }

        [Test]
        [Category("InputDataConverterTests")]
        [Description("Checks if ParsePair returns null if input is incorrect")]
        public void ParsePairJsonToDictionaryNullTest()
        {
            var inputStream = GetStreamFromString(IncorrectJsonString);
            Assert.IsNull(SystemUnderTest.ParsePair(inputStream));
        }

        [Test]
        [Category("InputDataConverterTests")]
        [Description("Checks if ParseBankTranslationPair parses VocabBank Translation pair correctly")]
        public void ParseBankTranslationPairTest()
        {
            var actual = SystemUnderTest.ParseBankTranslationPair(
                GetStreamFromString(TestBankTranslationPairCorrectJsonString));
            Assert.AreEqual(1, actual.VocabBankId);
            Assert.AreEqual(11, actual.Translation.Id);
        }

        [Test]
        [Category("InputDataConverterTests")]
        [Description("Checks if ParseBankTranslationPair returns null if input is incorrect")]
        public void ParseBankTranslationPairNullTest()
        {
            Assert.IsNull(SystemUnderTest.ParseBankTranslationPair(GetStreamFromString(IncorrectJsonString)));
        }

        [Test]
        [Category("InputDataConverterTests")]
        [Description("Checks if ParseBankHeaders gets name and description of the bank")]
        public void ParseBankHeadersTest()
        {
            var inputStream = GetStreamFromString(TestBankHeadersCorrectJsonString);
            var actual = SystemUnderTest.ParseBankHeaders(inputStream);
            Assert.AreEqual("Personality", actual.Name);
            Assert.AreEqual("Vocabulary bank about personality", actual.Description);
        }

        [Test]
        [Category("InputDataConverterTests")]
        [Description("Checks if ParseBankHeaders returns null if input string is incorrect")]
        public void ParseBankHeadersNullTest()
        {
            Assert.IsNull(SystemUnderTest.ParseBankHeaders(GetStreamFromString(IncorrectJsonString)));
        }

        private static MemoryStream GetStreamFromString(string stringToConvert)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(stringToConvert));
        }
    }
}
