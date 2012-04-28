using Autofac;
using NUnit.Framework;
using VX.Service.Repositories;
using VX.Service.Repositories.Interfaces;

namespace VX.Tests
{
    [TestFixture]
    internal class VocabBanksRepositoryTests : RepositoryTestsBase
    {
        public VocabBanksRepositoryTests()
        {
            ContainerBuilder.RegisterType<VocabBanksRepository>()
                .As<IVocabBanksRepository>()
                .InstancePerLifetimeScope();

            BuildContainer();
        }

        [Test]
        [Category("VocabBanksRepositoryTests")]
        [Description("Checks if method returns vocabbanks list")]
        public void GetVocabBanksTest()
        {
            var repositoryUnderTest = Container.Resolve<IVocabBanksRepository>();
            var actual = repositoryUnderTest.GetVocabBanks();
            Assert.AreEqual(actual.Count, 1);
        }
    }
}
