using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Moq;
using NUnit.Framework;
using VX.Domain.DataContracts;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Factories;
using VX.Service.Factories.Interfaces;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Tests
{
    [TestFixture]
    internal class TasksFactoryTests : TestsBase<ITasksFactory, TasksFactory>
    {
        public TasksFactoryTests()
        {
            ContainerBuilder.RegisterInstance(MockRandomPicker())
                .As<IRandomPicker>()
                .SingleInstance();

            ContainerBuilder.RegisterInstance(MockTaskValidator())
                .As<ITaskValidator>()
                .SingleInstance();

            ContainerBuilder.RegisterInstance(MockSynonymSelector())
                .As<ISynonymSelector>()
                .SingleInstance();

            BuildContainer();
        }

        [Test]
        [Category("TasksFactoryTests")]
        [Description("Checks if BuildTask returns correct task")]
        public void BuildTaskPositiveTest()
        {
            Assert.AreEqual(
                2,
                SystemUnderTest.BuildTask(new VocabBankContract
                                                   {
                                                       Translations = new List<ITranslation>
                                                                          {
                                                                              new TranslationContract
                                                                                  {
                                                                                      Source =
                                                                                          new WordContract
                                                                                              {
                                                                                                  Id = 1,
                                                                                                  Spelling
                                                                                                      =
                                                                                                      "Correct"
                                                                                              }
                                                                                  }
                                                                          }
                                                   }).Answers.Count);
        }

        [Test]
        [Category("TasksFactoryTests")]
        [Description("Checks if BuildTask throws ArgumentException if generated task is invalid")]
        public void BuildTaskNegativeTest()
        {
            Assert.Throws<ArgumentException>(() => SystemUnderTest.BuildTask(new VocabBankContract
                                                              {
                                                                  Translations = new List<ITranslation>
                                                                                     {
                                                                                         new TranslationContract
                                                                                             {
                                                                                                 Source = new WordContract
                                                                                                              {
                                                                                                                  Id = -1
                                                                                                              }
                                                                                             }
                                                                                     }
                                                              }));
        }

        [Test]
        [Category("TasksFactoryTests")]
        [Description("Checks if BuildTask inserts answer into answers in random order")]
        public void BuildTaskPositiveInsertAnswerTest()
        {
            IVocabBank vocabBank = new VocabBankContract();
            vocabBank.Translations = new List<ITranslation>
                                         {
                                             new TranslationContract
                                                 {
                                                     Id = 100,
                                                     Source = new WordContract
                                                                  {
                                                                      Id = 1,
                                                                      Spelling = "question"
                                                                  },
                                                     Target = new WordContract
                                                                  {
                                                                      Id = 2,
                                                                      Spelling = "questionAnswer"
                                                                  }
                                                 },
                                             new TranslationContract
                                                 {
                                                     Id = 200,
                                                     Source = new WordContract
                                                                  {
                                                                      Id = 3,
                                                                      Spelling =  "question2"
                                                                  },
                                                     Target = new WordContract
                                                                  {
                                                                      Id = 4,
                                                                      Spelling = "question2answer"
                                                                  }
                                                 },
                                             new TranslationContract
                                                 {
                                                     Id = 300,
                                                     Source = new WordContract
                                                                  {
                                                                      Id = 5,
                                                                      Spelling = "question3"
                                                                  },
                                                     Target = new WordContract
                                                                  {
                                                                      Id = 6,
                                                                      Spelling = "question3answer"
                                                                  }
                                                 }
                                         };

            Assert.AreEqual("questionAnswer", SystemUnderTest.BuildTask(vocabBank).Answers[0].Spelling);
        }

        private static IRandomPicker MockRandomPicker()
        {
            var mock = new Mock<IRandomPicker>();
            mock
                .Setup(item => item.PickItem(It.IsAny<IList<ITranslation>>()))
                .Returns((IList<ITranslation> item) => item.First());
            
            mock
                .Setup(
                    item =>
                    item.PickItems(It.IsAny<IList<ITranslation>>(), It.IsAny<int>(), It.IsAny<IList<ITranslation>>()))
                .Returns(
                    (IList<ITranslation> list, int numberOfItems, IList<ITranslation> blackList) => 
                        list.Skip(Math.Max(0, list.Count() - 2)).Take(2).ToList());
            mock
                .Setup(item => item.PickInsertIndex(It.IsAny<IList<ITranslation>>()))
                .Returns(0);
            return mock.Object;
        }

        private static ISynonymSelector MockSynonymSelector()
        {
            var mock = new Mock<ISynonymSelector>();
                mock
                    .Setup(item => item.GetSimilarTranslations(It.IsAny<ITranslation>(), It.IsAny<IList<ITranslation>>()))
                    .Returns(new List<ITranslation>());
            return mock.Object;
        }

        private static ITaskValidator MockTaskValidator()
        {
            var mock = new Mock<ITaskValidator>();
            mock
                .Setup(item => item.IsValidTask(It.Is<ITask>(task => task.Question.Id != -1)))
                .Returns(true);
            mock
                .Setup(item => item.IsValidTask(It.Is<ITask>(task => task.Question.Id == -1)))
                .Returns(false);

            return mock.Object;
        }
    }
}
