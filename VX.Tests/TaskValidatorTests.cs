using System.Collections.Generic;
using NUnit.Framework;
using VX.Domain.Entities;
using VX.Domain.Entities.Impl;
using VX.Service.Infrastructure;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Tests
{
    [TestFixture]
    internal class TaskValidatorTests :TestsBase<ITaskValidator, TaskValidator>
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

        public TaskValidatorTests()
        {
            BuildContainer();
        }

        [Test]
        [Category("TaskTest")]
        [Description("Checks if validation of task returns true if answer presents in options")]
        public void IsValidTaskPositiveTest()
        {
            Assert.IsTrue(SystemUnderTest.IsValidTask(
                BuildTask(QuestionWord, AnswerWord, TranslationOptionsWithAnswer)));
        }

        [Test]
        [Category("TaskTest")]
        [Description("Checks if validation of the task fails if no answer in options")]
        public void IsValidTaskNegativeTest()
        {
            Assert.IsFalse(SystemUnderTest.IsValidTask(
                BuildTask(QuestionWord, AnswerWord, TranslationOptionsNoAnswer)));
        }

        [Test]
        [Category("TaskTest")]
        [Description("Check if validation fails if question or answer is null")]
        public void IsValidTaskNegativeNullsTest()
        {
            var task = BuildTask(null, null, null);
            Assert.IsFalse(SystemUnderTest.IsValidTask(task));
            task.CorrectAnswer = AnswerWord;
            Assert.IsFalse(SystemUnderTest.IsValidTask(task));
            task.CorrectAnswer = null;
            task.Question = QuestionWord;
            Assert.IsFalse(SystemUnderTest.IsValidTask(task));
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
