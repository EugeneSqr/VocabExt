using Autofac;
using NUnit.Framework;
using VX.Service.Infrastructure.Factories.Entities;
using VX.Service.Infrastructure.Factories.Surrogates;
using VX.Tests.Mocks;

namespace VX.Tests.FactoriesTests
{
    [TestFixture]
    public class SurrogatesFactoryTests : SerializerFactoryTests<ISurrogatesFactory, SurrogatesFactory>
    {
        public SurrogatesFactoryTests()
        {
            ContainerBuilder.RegisterType<EntitiesFactoryMock>()
                .As<IAbstractEntitiesFactory>()
                .InstancePerLifetimeScope();
            
            BuildContainer();
        }

        [Test]
        [Category("EntitiesFactory.Entities.Stream")]
        [Description("Creates language from correct stream")]
        public void CreateLanguageCorrectStreamTest()
        {
            var actual = SystemUnderTest.CreateVocabBankSummary(CorrectStream);
            Assert.AreEqual(OutVocabBankSummary.Id, actual.Id);
        }

        [Test]
        [Category("EntitiesFactory.Entities.Stream")]
        [Description("Creates empty language from incorrect stream")]
        public void CreateEmptyLanguageIncorrectStreamTest()
        {
            CheckVocabBankSummary(0, null, null, SystemUnderTest.CreateVocabBankSummary(null));
        }

        [Test]
        [Category("Surrogates.Empty")]
        [Description("Creates empty vocabbanksummary")]
        public void CreateEmptyVocabBankSummary()
        {
            var actual = SystemUnderTest.CreateVocabBankSummary();
            CheckVocabBankSummary(0, null, null, actual);
        }
    }
}
