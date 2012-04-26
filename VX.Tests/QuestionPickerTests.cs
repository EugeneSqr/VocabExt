using Autofac;
using NUnit.Framework;
using VX.Service;
using VX.Tests.Mocks;

namespace VX.Tests
{
    [TestFixture]
    internal class QuestionPickerTests
    {
        private readonly IContainer container;

        public QuestionPickerTests()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<RandomFacadeMock>()
                .As<IRandomFacade>()
                .InstancePerLifetimeScope();

            builder.RegisterType<QuestionPicker>()
                .As<IQuestionPicker>()
                .InstancePerLifetimeScope();

            container = builder.Build();
        }

        [Test]
        [Category("QuestionPickerTests")]
        [Description("Checks if question picker throws and ArgumentException if input comtainer null or empty")]
        public void PickQuestionNagativeTest()
        {
            var pickerUnderTest = container.Resolve<IQuestionPicker>();
            /*pickerUnderTest.PickQuestion()*/
        }
    }
}
