using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Moq;
using VX.Domain.Entities;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Tests.TastsFactoryTests
{
    internal abstract class MultiDirectionsForwardFixture : MultiDirectionsFixture
    {
        protected MultiDirectionsForwardFixture()
        {
            RandomPickerMock.Setup(item => item.PickItem(It.IsAny<IList<int>>()))
                .Returns(1);

            ContainerBuilder.RegisterInstance(RandomPickerMock.Object)
                .As<IRandomPicker>()
                .SingleInstance();
        }
    }
}
