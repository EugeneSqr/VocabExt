using System.Linq;
using NUnit.Framework;
using VX.Domain.DataContracts;
using VX.Service.Repositories;
using VX.Service.Repositories.Interfaces;

namespace VX.Tests.RepositoriesTests
{
    [TestFixture]
    internal class TranslationsRepositoryTest : RepositoryTestsBase<ITranslationsRepository, TranslationsRepository>
    {
        public TranslationsRepositoryTest()
        {
            BuildContainer();
        }

        [Test]
        [Category("TranslationsRepositoryTests")]
        [Description("Checks if GetTranslations returns list of translations if parameters are correct")]
        public void GetTranslationsPositiveTest()
        {
            var actual = SystemUnderTest.GetTranslations("1");
            Assert.Greater(actual.Count, 0);
        }

        [Test]
        [Category("TranslationsRepositoryTests")]
        [Description("Checks if GetTranslations returns empty list if id is incorrect")]
        public void GetTranslationsNegativeBadIdTest()
        {
            var actual = SystemUnderTest.GetTranslations("asd");
            Assert.AreEqual(0, actual.Count);
        }

        [Test]
        [Category("TranslationsRepositoryTests")]
        [Description("Checks of UpdateTranslation updates translation correctly")]
        public void UpdateTranslationTest()
        {
            TranslationContract translation = new TranslationContract { Id = 1,
            Source = new WordContract
                         {
                             Id = 1
                         },
            Target = new WordContract
                         {
                             Id = 4
                         }};

            Assert.IsTrue(SystemUnderTest.UpdateTranslation(translation));
            var actual = SystemUnderTest.GetTranslations("1").First(item => item.Id == 1);
            Assert.AreEqual(1, actual.Source.Id);
            Assert.AreEqual(4, actual.Target.Id);
        }
    }
}