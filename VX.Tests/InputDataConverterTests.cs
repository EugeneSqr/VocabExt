using System.IO;
using System.Text;
using NUnit.Framework;
using VX.Service.Infrastructure;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Tests
{
    [TestFixture]
    public class InputDataConverterTests : TestsBase<IInputDataConverter, InputDataConverter>
    {
        private const string TestTranslationCorrectJsonString =
            "{\"__type\":\"TranslationContract:#VX.Domain.DataContracts\",\"Id\":1,\"Source\":{\"__type\":\"WordContract:#VX.Domain.DataContracts\",\"Id\":1,\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.DataContracts\",\"Id\":1,\"Name\":\"english\",\"Abbreviation\":\"en\"},\"Spelling\":\"ambitious\",\"Transcription\":\"æm'bɪʃəs\"},\"Target\":{\"__type\":\"WordContract:#VX.Domain.DataContracts\",\"Id\":2,\"Language\":{\"__type\":\"LanguageContract:#VX.Domain.DataContracts\",\"Id\":2,\"Name\":\"russian\",\"Abbreviation\":\"ru\"},\"Spelling\":\"честолюбивый\",\"Transcription\":\"ч'истал'уб'`ивый'\"}}";

        private const string TestTranslationIncorrectJsonString = "\asdf%as$d!k@h]";
            
        
        public InputDataConverterTests()
        {
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
        [Description("Checks if Convert returns ITranslation if input is correct")]
        public void ConvertJsonToTranslationTest()
        {
            MemoryStream inputStream = new MemoryStream(Encoding.UTF8.GetBytes(TestTranslationCorrectJsonString));
            Assert.AreEqual(1, SystemUnderTest.Convert(inputStream).Id);
        }

        [Test]
        [Category("InputDataConverterTests")]
        [Description("Checks if Convert returns null if input is incorrect")]
        public void ConvertJsonToTranslationNullTest()
        {
            MemoryStream inputStream = new MemoryStream(Encoding.UTF8.GetBytes(TestTranslationIncorrectJsonString));
            Assert.IsNull(SystemUnderTest.Convert(inputStream));
        }
    }
}
