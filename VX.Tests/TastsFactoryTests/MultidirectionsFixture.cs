using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Moq;
using VX.Domain.Entities;
using VX.Domain.Entities.Impl;
using VX.Service.Infrastructure.Factories.Tasks;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Tests.TastsFactoryTests
{
    internal abstract class MultiDirectionsFixture : TestsBase<ITasksFactory, MultiDirectionsTasksFactory>
    {
        protected Mock<IRandomPicker> RandomPickerMock;
        
        protected MultiDirectionsFixture()
        {
            ContainerBuilder.RegisterInstance(MockSynonymSelector())
                .As<ISynonymSelector>()
                .SingleInstance();

            RandomPickerMock = new Mock<IRandomPicker>();
            RandomPickerMock
                .Setup(item => item.PickItem(It.IsAny<IList<ITranslation>>()))
                .Returns((IList<ITranslation> item) => item.First());
            RandomPickerMock
                .Setup(
                    item =>
                    item.PickItems(It.IsAny<IList<ITranslation>>(), It.IsAny<int>(), It.IsAny<IList<ITranslation>>()))
                .Returns(
                    (IList<ITranslation> list, int numberOfItems, IList<ITranslation> blackList) =>
                    list.Skip(Math.Max(0, list.Count() - 2)).Take(2).ToList());
            RandomPickerMock
                .Setup(item => item.PickInsertIndex(It.IsAny<List<IWord>>()))
                .Returns(0);
        }

        protected readonly IVocabBank Input = new VocabBankContract
        {
            Translations = new List<ITranslation>
                               {
                                                    new TranslationContract
                                                        {
                                                            Id = 100, 
                                                            Source = new WordContract { Id = 1, Spelling = "source1" },
                                                            Target = new WordContract { Id = 2, Spelling = "target1" }
                                                        },
                                                    new TranslationContract
                                                        {
                                                            Id = 200,
                                                            Source = new WordContract { Id = 3, Spelling = "source2" },
                                                            Target = new WordContract { Id = 4, Spelling = "target2" }
                                                        },
                                                    new TranslationContract
                                                        {
                                                            Id = 300,
                                                            Source = new WordContract { Id = 5, Spelling = "source3" },
                                                            Target = new WordContract { Id = 6, Spelling = "target3" }
                                                        },
                                                    new TranslationContract
                                                        {
                                                            Id = 400,
                                                            Source = new WordContract { Id = 7, Spelling = "source4" },
                                                            Target = new WordContract { Id = 8, Spelling = "target4" }
                                                        }
                                                }
        };

        private static ISynonymSelector MockSynonymSelector()
        {
            var mock = new Mock<ISynonymSelector>();
            mock
                .Setup(item => item.GetSimilarTranslations(It.IsAny<ITranslation>(), It.IsAny<IList<ITranslation>>()))
                .Returns(new List<ITranslation>());
            return mock.Object;
        }
    }
}
