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
        private static readonly IWord QuestionWord = new WordContract
        {
            Id = 1
        };

        private static readonly IWord AnswerWord = new WordContract
        {
            Id = 2
        };
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
            var factoryUnderTest = container.Resolve<ITasksFactory>();
            Assert.AreEqual(2, factoryUnderTest.BuildTask(new VocabBankContract
                                                              {
                                                                  Translations = new List<ITranslation>
                                                                                     {
                                                                                         new TranslationContract
                                                                                             {
                                                                                                 Source = QuestionWord
                                                                                             }
                                                                                     }
                                                              }).Answers.Count);
        }

        [Test]
        [Category("TasksFactoryTests")]
        [Description("Check if BuildTask throws ArgumentException if generated task is invalid")]
        public void BuildTaskNegativeTest()
        {
            var factoryUnderTest = container.Resolve<ITasksFactory>();
            Assert.Throws<ArgumentException>(() => factoryUnderTest.BuildTask(new VocabBankContract
                                                              {
                                                                  Translations = new List<ITranslation>
                                                                                     {
                                                                                         new TranslationContract
                                                                                             {
                                                                                                 Source = AnswerWord
                                                                                             }
                                                                                     }
                                                              }));
        }

        private static Mock<IRandomPicker> MockRandomPicker()
        {
            var mock = new Mock<IRandomPicker>();
            mock
                .Setup(item => item.PickItem(It.IsAny<IList<ITranslation>>()))
                .Returns((IList<ITranslation> item) => item.First());
            mock
                .Setup(
                    item =>
                    item.PickItems(It.IsAny<IList<ITranslation>>(), It.IsAny<int>(), It.IsAny<IList<ITranslation>>()))
                .Returns(new List<ITranslation>
                             {
                                 new TranslationContract
                                     {
                                         Id = 2, 
                                         Source = AnswerWord, 
                                         Target = QuestionWord
                                     }
                             });
            return mock;
        }

        private static Mock<ITaskValidator> MockTaskValidator()
        {
            var mock = new Mock<ITaskValidator>();
            mock
                .Setup(item => item.IsValidTask(It.Is<ITask>(task => task.Question.Id == 1)))
                .Returns(true);
            mock
                .Setup(item => item.IsValidTask(It.Is<ITask>(task => task.Question.Id == 2)))
                .Returns(false);

            return mock;
        }
    }
}
