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
        private const string TestUpdateTranslationIncorrectJsonString = "\asdf%as$d!k@h]";

        private const string TestParsePairCorrectJsonString = "{\"parent\":\"1\", \"child\": 2}";
            
        
        public InputDataConverterTests()
        {
            ContainerBuilder.RegisterInstance(new EntitiesFactoryMock())
                .As<IEntitiesFactory>()
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
        [Description("Checks if ParsePair parses input json strin with key - value pair inside")]
        public void ParsePairJsonToDictionaryTest()
        {
            MemoryStream inputstream = new MemoryStream(Encoding.UTF8.GetBytes(TestParsePairCorrectJsonString));
            var actual = SystemUnderTest.ParsePair(inputstream);
            Assert.AreEqual(1, actual.ParentId);
            Assert.AreEqual(2, actual.ChildId);
        }

        [Test]
        [Category("InputDataConverterTests")]
        [Description("Checks if ParsePair returns null if input is incorrect")]
        public void ParsePairJsonToDictionaryNullTest()
        {
            MemoryStream inputstream = new MemoryStream(Encoding.UTF8.GetBytes(TestUpdateTranslationIncorrectJsonString));
            Assert.IsNull(SystemUnderTest.ParsePair(inputstream));
        }
    }
}
