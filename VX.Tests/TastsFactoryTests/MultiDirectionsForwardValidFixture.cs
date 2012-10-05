using Autofac;
using Moq;
using NUnit.Framework;
using VX.Domain.Entities;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Tests.TastsFactoryTests
{
    [TestFixture(Description = "Forward direction fixture. Validator returns always valid.")]
    internal class MultiDirectionsForwardValidFixture : MultiDirectionsForwardFixture
    {
        public MultiDirectionsForwardValidFixture()
        {
            ContainerBuilder.RegisterInstance(MockTaskValidator())
                .As<ITaskValidator>()
                .SingleInstance();

            BuildContainer();
        }

        [Test]
        [Category("MultiDirectionsTasksFactoryTests")]
        [Description("BuildTask builds the correct task")]
        public void BuildTaskTest()
        {
            var actual = SystemUnderTest.BuildTask(Input);
            Assert.AreEqual(1, actual.Question.Id);
            Assert.AreEqual(2, actual.CorrectAnswer.Id);
            Assert.AreEqual(3, actual.Answers.Count);
            Assert.AreEqual(2, actual.Answers[0].Id);
            Assert.AreEqual(6, actual.Answers[1].Id);
            Assert.AreEqual(8, actual.Answers[2].Id);
        }

        private static ITaskValidator MockTaskValidator()
        {
            var mock = new Mock<ITaskValidator>();
            mock
                .Setup(item => item.IsValidTask(It.IsAny<ITask>())).Returns(true);
            return mock.Object;
        }
    }
}
