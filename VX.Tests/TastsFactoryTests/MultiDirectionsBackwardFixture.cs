using System.Collections.Generic;
using Autofac;
using Moq;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Tests.TastsFactoryTests
{
    internal abstract class MultiDirectionsBackwardFixture : MultiDirectionsFixture
    {
        protected MultiDirectionsBackwardFixture()
        {
            RandomPickerMock.Setup(item => item.PickItem(It.IsAny<IList<int>>()))
                .Returns(0);

            ContainerBuilder.RegisterInstance(RandomPickerMock.Object)
                .As<IRandomPicker>()
                .SingleInstance();
        }
    }
}
