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
    internal class TaskValidatorTests
    {
        private static readonly IWord QuestionWord = new WordContract
        {
            Id = 1
        };

        private static readonly IWord AnswerWord = new WordContract
        {
            Id = 2
        };

        private static readonly IList<IWord> TranslationOptionsWithAnswer = new List<IWord>
                                                                                {
                                                                                    new WordContract
                                                                                        {
                                                                                            Id = 3
                                                                                        },
                                                                                    AnswerWord,
                                                                                };

        private static readonly IList<IWord> TranslationOptionsNoAnswer = new List<IWord>
                                                                              {
                                                                                  new WordContract
                                                                                      {
                                                                                          Id = 3
                                                                                      },
                                                                                  new WordContract
                                                                                      {
                                                                                          Id = 4
                                                                                      },
                                                                              };

        private readonly IContainer container;

        public TaskValidatorTests()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TaskValidator>()
                .As<ITaskValidator>()
                .InstancePerLifetimeScope();

            container = builder.Build();
        }

        [Test]
        [Category("TaskTest")]
        [Description("Checks if validation of task returns true if answer presents in options")]
        public void IsValidTaskPositiveTest()
        {
            var validatorUnderTest = container.Resolve<ITaskValidator>();
            Assert.IsTrue(validatorUnderTest.IsValidTask(
                BuildTask(QuestionWord, AnswerWord, TranslationOptionsWithAnswer)));
        }

        [Test]
        [Category("TaskTest")]
        [Description("Checks if validation of the task fails if no answer in options")]
        public void IsValidTaskNegativeTest()
        {
            var validatorUnderTest = container.Resolve<ITaskValidator>();
            Assert.IsFalse(validatorUnderTest.IsValidTask(
                BuildTask(QuestionWord, AnswerWord, TranslationOptionsNoAnswer)));
        }

        [Test]
        [Category("TaskTest")]
        [Description("Check if validation fails if question or answer is null")]
        public void IsValidTaskNegativeNullsTest()
        {
            var validatorUnderTest = container.Resolve<ITaskValidator>();
            var task = BuildTask(null, null, null);
            Assert.IsFalse(validatorUnderTest.IsValidTask(task));
            task.CorrectAnswer = AnswerWord;
            Assert.IsFalse(validatorUnderTest.IsValidTask(task));
            task.CorrectAnswer = null;
            task.Question = QuestionWord;
            Assert.IsFalse(validatorUnderTest.IsValidTask(task));
        }

        private static ITask BuildTask(IWord question, IWord answer, IList<IWord> options)
        {
            return new TaskContract
            {
                Question = question,
                CorrectAnswer = answer,
                Answers = options
            };
        }
    }
}
