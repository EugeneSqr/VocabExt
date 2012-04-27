using System;
using System.Collections.Generic;
using Autofac;
using NUnit.Framework;
using VX.Service;
using VX.Service.Interfaces;
using VX.Tests.Mocks;

namespace VX.Tests
{
    [TestFixture]
    internal class RandomPickerTests
    {
        private readonly IContainer container;

        public RandomPickerTests()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<RandomFacadeMock>()
                .As<IRandomFacade>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RandomPicker>()
                .As<IRandomPicker>()
                .InstancePerLifetimeScope();

            container = builder.Build();
        }

        [Test]
        [Category("RandomPickerTests")]
        [Description("Checks if PickItem throws an ArgumentNullException if list is null")]
        public void PickItemNegativeListNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => GetSystemUnderTest().PickItem<IList<string>>(null));
        }

        [Test]
        [Category("RandomPickerTests")]
        [Description("Checks if PickItem throws an ArgumentNullException if list is empty")]
        public void PickItemNegativeListEmptyTest()
        {
            Assert.Throws<ArgumentNullException>(() => GetSystemUnderTest().PickItem(new List<string>()));
        }

        [Test]
        [Category("RandomPickerTests")]
        [Description("Checks if PickItems throws an ArgumentNullException if list is null")]
        public void PickItemsNegativeListNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => GetSystemUnderTest().PickItems<IList<string>>(null, 1));
        }

        [Test]
        [Category("RandomPickerTests")]
        [Description("Checks if PickItems throws an ArgumentNullException if list is empty")]
        public void PickItemsNegativeListEmptyTest()
        {
            Assert.Throws<ArgumentNullException>(() => GetSystemUnderTest().PickItems(new List<string>(), 1));
        }

        [Test]
        [Category("RandomPickerTests")]
        [Description("Checks if PickItems throws an ArgumentOutOfRangeException if n < 1")]
        public void PickItemsNegativeNTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => GetSystemUnderTest().PickItems(new List<string> { "a", "b" }, -1));
        }

        private IRandomPicker GetSystemUnderTest()
        {
            return container.Resolve<IRandomPicker>();
        }
    }
}
