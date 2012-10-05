using System;
using Autofac;
using Moq;
using NUnit.Framework;
using VX.Domain.Entities;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Tests.TastsFactoryTests
{
    [TestFixture(Description = "Backward direction fixture. Validator returns always invalid.")]
    internal class MultiDirectionsBackwardInvalidFixture : MultiDirectionsBackwardFixture
    {
        public MultiDirectionsBackwardInvalidFixture()
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
            Assert.Throws<ArgumentException>(() => SystemUnderTest.BuildTask(Input));
        }

        private static ITaskValidator MockTaskValidator()
        {
            var mock = new Mock<ITaskValidator>();
            mock
                .Setup(item => item.IsValidTask(It.IsAny<ITask>()))
                .Returns(false);
            return mock.Object;
        }
    }
}
