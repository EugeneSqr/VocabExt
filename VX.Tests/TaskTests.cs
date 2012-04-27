using System.Collections.Generic;
using NUnit.Framework;
using VX.Domain;
using VX.Domain.Interfaces;
using VX.Domain.Interfaces.Entities;
using VX.Domain.Surrogates;

namespace VX.Tests
{
    [TestFixture]
    internal class TaskTests
    {
        private static readonly IWord QuestionWord = new WordSurrogate
                                                         {
                                                             Id = 1
                                                         };

        private static readonly IWord AnswerWord = new WordSurrogate
                                                       {
                                                           Id = 2
                                                       };

        private static readonly IList<IWord> TranslationOptionsWithAnswer = new List<IWord>
                                                                                {
                                                                                    new WordSurrogate
                                                                                        {
                                                                                            Id = 3
                                                                                        },
                                                                                    AnswerWord,
                                                                                };

        private static readonly IList<IWord> TranslationOptionsNoAnswer = new List<IWord>
                                                                              {
                                                                                  new WordSurrogate
                                                                                      {
                                                                                          Id = 3
                                                                                      },
                                                                                  new WordSurrogate
                                                                                      {
                                                                                          Id = 4
                                                                                      },
                                                                              };
        [Test]
        [Category("TaskTest")]
        [Description("Checks if validation of task returns true if answer presents in options")]
        public void IsValidTaskPositiveTest()
        {
            var taskUnderTest = BuildTask(QuestionWord, AnswerWord, TranslationOptionsWithAnswer);
            Assert.IsTrue(taskUnderTest.IsValidTask());
        }

        [Test]
        [Category("TaskTest")]
        [Description("Checks if validation of the task fails if no answer in options")]
        public void IsValidTaskNegativeTest()
        {
            var taskUnderTest = BuildTask(QuestionWord, AnswerWord, TranslationOptionsNoAnswer);
            Assert.IsFalse(taskUnderTest.IsValidTask());
        }

        [Test]
        [Category("TaskTest")]
        [Description("Check if validation fails if question or answer is null")]
        public void IsValidTaskNegativeNullsTest()
        {
            var taskUnderTest = BuildTask(null, null, null);
            Assert.IsFalse(taskUnderTest.IsValidTask());
            taskUnderTest.CorrectAnswer = AnswerWord;
            Assert.IsFalse(taskUnderTest.IsValidTask());
            taskUnderTest.CorrectAnswer = null;
            taskUnderTest.Question = QuestionWord;
            Assert.IsFalse(taskUnderTest.IsValidTask());
        }
        
        [Test]
        [Category("TaskTest")]
        [Description("Checks if correct answer logic returns true if answer is correct")]
        public void IsCorrectAnswerPositiveTest()
        {
            var taskUnderTest = BuildTask(QuestionWord, AnswerWord, TranslationOptionsWithAnswer);
            Assert.IsTrue(taskUnderTest.IsCorrectAnswer(AnswerWord));
        }

        [Test]
        [Category("TaskTest")]
        [Description("Checks if correct answer logic returns false if answer is incorrect")]
        public void IsCorrectAnswerNegativeTest()
        {
            var taskUnderTest = BuildTask(QuestionWord, AnswerWord, TranslationOptionsWithAnswer);
            Assert.IsFalse(taskUnderTest.IsCorrectAnswer(QuestionWord));
        }

        private ITask BuildTask(IWord question, IWord answer, IList<IWord> options)
        {
            return new Task
                       {
                           Question = question,
                           CorrectAnswer = answer,
                           Answers = options
                       };
        }
    }
}