using System;
using System.Collections.Generic;
using Autofac;
using NUnit.Framework;
using VX.Service;
using VX.Service.Infrastructure;
using VX.Service.Infrastructure.Interfaces;
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
        [Description("Checks if PickItem returns item from list")]
        public void PickItemPositiveTest()
        {
            Assert.AreNotEqual(GetSystemUnderTest().PickItem(new List<string> {"item1", "item2"}), null);
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

        [Test]
        [Category("RandomPickerTests")]
        [Description("Checks if PickItems returns three different items from list of three items")]
        public void PickItemsPositiveThreeOfThreeTest()
        {
            var actual = GetSystemUnderTest().PickItems(new List<string> {"item1", "item2", "item3"}, 3);
            Assert.AreEqual(3, actual.Count);
            Assert.AreNotEqual(actual[0], actual[1]);
            Assert.AreNotEqual(actual[0], actual[2]);
            Assert.AreNotEqual(actual[1], actual[2]);
        }

        [Test]
        [Category("RandomPickerTests")]
        [Description("Checks if PickItems returns two different items from list of two items even if requesting three")]
        public void PickItemsPositiveTwoOfTwoMaximum()
        {
            var actual = GetSystemUnderTest().PickItems(new List<string> {"item1", "item2"}, 3);
            Assert.AreEqual(2, actual.Count);
            Assert.AreNotEqual(actual[0], actual[1]);
        }

        [Test]
        [Category("RandomPickerTests")]
        [Description("Checks if PickItems returns two different items from list of two items even if requesting three and blacklist is null")]
        public void PickItemsPositiveTwoOfTwoMaximumNullBlackList()
        {
            var actual = GetSystemUnderTest().PickItems(new List<string> { "item1", "item2" }, 3, null);
            Assert.AreEqual(2, actual.Count);
            Assert.AreNotEqual(actual[0], actual[1]);
        }

        [Test]
        [Category("RandomPickerTests")]
        [Description("Checks if PickItems returns two different items from list of two items even if requesting three and blacklist is empty")]
        public void PickItemsPositiveTwoOfTwoMaximumEmptyBlackList()
        {
            var actual = GetSystemUnderTest().PickItems(new List<string> { "item1", "item2" }, 3, new List<string>());
            Assert.AreEqual(2, actual.Count);
            Assert.AreNotEqual(actual[0], actual[1]);
        }

        [Test]
        [Category("RandomPickerTests")]
        [Description("Checks if PickItems returns empty list if all items blacklisted (lists are the same)")]
        public void PickItemsPositiveEmptyListSameLists()
        {
            var list = new List<string> {"item1", "item2", "item3"};
            var actual = GetSystemUnderTest().PickItems(list, 2, list);
            Assert.AreEqual(0, actual.Count);
        }

        [Test]
        [Category("RandomPickerTests")]
        [Description("Checks if PickItems returns empty list if all items blacklisted")]
        public void PickItemsPositiveEmptyListIfAllItemsBlacklistedTest()
        {
            var actual = GetSystemUnderTest().PickItems(
                new List<string> { "item1", "item2", "item3" }, 
                2, 
                new List<string> { "item1", "item2", "item3" });
            Assert.AreEqual(0, actual.Count);
        }

        [Test]
        [Category("RandomPickerTests")]
        [Description("Checks if PickItems returns two different items form list of four items even if requesting 3 and excluding two")]
        public void PickItemsPositiveTwoOfFourExcludigTwoMaximum()
        {
            var actual = GetSystemUnderTest().PickItems(
                new List<string> {"item1", "item2", "item3", "item4"}, 
                3, 
                new List<string> {"item1", "item4"});

            Assert.AreEqual(2, actual.Count);
            Assert.IsTrue(actual.Contains("item2"));
            Assert.IsTrue(actual.Contains("item3"));
        }

        private IRandomPicker GetSystemUnderTest()
        {
            return container.Resolve<IRandomPicker>();
        }
    }
}
