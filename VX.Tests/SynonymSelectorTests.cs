using System.Collections.Generic;
using Autofac;
using NUnit.Framework;
using VX.Domain.DataContracts;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Infrastructure;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Tests
{
    [TestFixture]
    internal class SynonymSelectorTests
    {
        private readonly IContainer container;

        private IList<ITranslation> testTranslationsList;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            testTranslationsList = new List<ITranslation>
                                                    {
                                                        new TranslationContract
                                                            {
                                                                Id = 1,
                                                                Source = new WordContract
                                                                             {
                                                                                 Id = 1,
                                                                                 Spelling = "source1"
                                                                             },
                                                                Target = new WordContract
                                                                             {
                                                                                 Id = 2,
                                                                                 Spelling = "target2"
                                                                             }
                                                            },
                                                        new TranslationContract
                                                            {
                                                                Id = 2,
                                                                Source = new WordContract
                                                                             {
                                                                                 Id = 1,
                                                                                 Spelling = "source1"
                                                                             },
                                                                Target = new WordContract
                                                                             {
                                                                                 Id = 3,
                                                                                 Spelling = "target3"
                                                                             }
                                                            },
                                                        new TranslationContract
                                                            {
                                                                Id = 3,
                                                                Source = new WordContract
                                                                             {
                                                                                 Id = 4,
                                                                                 Spelling = "source4"
                                                                             },
                                                                Target = new WordContract
                                                                             {
                                                                                 Id = 2,
                                                                                 Spelling = "target2"
                                                                             }
                                                            },
                                                        new TranslationContract
                                                            {
                                                                Id = 4,
                                                                Source = new WordContract
                                                                             {
                                                                                 Id = 5,
                                                                                 Spelling = "source5"
                                                                             },
                                                                Target = new WordContract
                                                                             {
                                                                                 Id = 6,
                                                                                 Spelling = "target6"
                                                                             }
                                                            }
                                                    };
        }

        public SynonymSelectorTests()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<SynonymSelector>()
                .As<ISynonymSelector>()
                .InstancePerLifetimeScope();

            container = builder.Build();
        }

        [Test]
        [Category("SynonymSelectorTests")]
        [Description("Checks if GetSimilarTranslations selects all similar translations")]
        public void GetSimilarTranslationsTest()
        {
            var synonymSelector = container.Resolve<ISynonymSelector>();
            var actual = synonymSelector.GetSimilarTranslations(testTranslationsList[0], testTranslationsList);
            Assert.AreEqual(3, actual.Count);
            Assert.AreEqual(1, actual[0].Id);
            Assert.AreEqual(2, actual[1].Id);
            Assert.AreEqual(3, actual[2].Id);
        }
    }
}
