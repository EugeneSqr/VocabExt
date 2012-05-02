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
    internal class TasksFactoryTests
    {
        private readonly IContainer container;
        
        public TasksFactoryTests()
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(MockRandomPicker().Object)
                .As<IRandomPicker>()
                .SingleInstance();

            builder.RegisterInstance(MockTaskValidator().Object)
                .As<ITaskValidator>()
                .SingleInstance();

            builder.RegisterType<TasksFactory>()
                .As<ITasksFactory>()
                .InstancePerLifetimeScope();

            container = builder.Build();
        }

        [Test]
        [Category("TasksFactoryTests")]
        [Description("Checks if BuildTask returns correct task")]
        public void BuildTaskPositiveTest()
        {
            Assert.AreEqual(
                2,
                GetSystemUnderTest().BuildTask(new VocabBankContract
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
            Assert.Throws<ArgumentException>(() => GetSystemUnderTest().BuildTask(new VocabBankContract
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
        [Description("Checks if BuildTask inserts answer into answers in ranfom order")]
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

            Assert.AreEqual("questionAnswer", GetSystemUnderTest().BuildTask(vocabBank).Answers[0].Spelling);
        }

        private ITasksFactory GetSystemUnderTest()
        {
            return container.Resolve<ITasksFactory>();
        }

        private static Mock<IRandomPicker> MockRandomPicker()
        {
            var mock = new Mock<IRandomPicker>();
            mock
                .Setup(item => item.PickItem(It.IsAny<IList<ITranslation>>()))
                .Returns((IList<ITranslation> item) => item.First());
            
            // taking last 2 items of input list
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
            return mock;
        }

        private static Mock<ITaskValidator> MockTaskValidator()
        {
            var mock = new Mock<ITaskValidator>();
            mock
                .Setup(item => item.IsValidTask(It.Is<ITask>(task => task.Question.Id != -1)))
                .Returns(true);
            mock
                .Setup(item => item.IsValidTask(It.Is<ITask>(task => task.Question.Id == -1)))
                .Returns(false);

            return mock;
        }
    }
}
