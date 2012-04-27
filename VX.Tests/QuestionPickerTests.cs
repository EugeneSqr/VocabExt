using System;
using System.Collections.Generic;
using Autofac;
using Moq;
using NUnit.Framework;
using VX.Domain.Interfaces.Entities;
using VX.Domain.Surrogates;
using VX.Service;
using VX.Service.Interfaces;
using VX.Tests.Mocks;

namespace VX.Tests
{
    [TestFixture]
    internal class QuestionPickerTests
    {
        private readonly IContainer container;

        public QuestionPickerTests()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<RandomFacadeMock>()
                .As<IRandomFacade>()
                .InstancePerLifetimeScope();
            
            builder.RegisterInstance(CreateRandomPickerMock().Object)
                .As<IRandomPicker>()
                .SingleInstance();

            builder.RegisterType<QuestionPicker>()
                .As<IQuestionPicker>()
                .InstancePerLifetimeScope();

            container = builder.Build();
        }

        [Test]
        [Category("QuestionPickerTests")]
        [Description("Checks if question picker returns question word")]
        public void PickQuestionPositive()
        {
            var pickerUnderTest = container.Resolve<IQuestionPicker>();
            Assert.AreEqual(pickerUnderTest.PickQuestion(BuildTestVocabBank(5)).Id, 1);
        }

        [Test]
        [Category("QuestionPickerTests")]
        [Description("Checks if question picker throws an ArgumentNullException if input container null")]
        public void PickQuestionNegativeNullTest()
        {
            var pickerUnderTest = container.Resolve<IQuestionPicker>();
            Assert.Throws<ArgumentNullException>(() => pickerUnderTest.PickQuestion(null));
        }

        [Test]
        [Category("QuestionPickerTests")]
        [Description("Checks if question picker throws an ArgumentNullException if input container is empty")]
        public void PickQuestionNegativeEmptyTest()
        {
            var pickerUnderTest = container.Resolve<IQuestionPicker>();
            Assert.Throws<ArgumentNullException>(() => pickerUnderTest.PickQuestion(new VocabBankSurrogate()));
        }

        private static IVocabBank BuildTestVocabBank(int id)
        {
            return new VocabBankSurrogate
                       {
                           Id = id,
                           Name = "TestBank",
                           Description = "TestDescription",
                           Tags = new List<ITag>(),
                           Translations = new List<ITranslation>
                                              {
                                                  new TranslationSurrogate
                                                      {
                                                          Id = 1,
                                                          Source = new WordSurrogate
                                                                       {
                                                                           Id = 1,
                                                                           Spelling = "SourceWord1"
                                                                       },
                                                          Target = new WordSurrogate
                                                                       {
                                                                           Id = 2,
                                                                           Spelling = "TargetWord2"
                                                                       }
                                                      }
                                              }
                       };
        }

        private static Mock<IRandomPicker> CreateRandomPickerMock()
        {
            var randomPickerMock = new Mock<IRandomPicker>();
            var testWord = new WordSurrogate {Id = 1};
            randomPickerMock
                .Setup(item => item.PickItem(It.IsAny<IList<IWord>>()))
                .Returns(testWord);
            randomPickerMock
                .Setup(item => item.PickItem(It.IsAny<IList<ITranslation>>()))
                .Returns(new TranslationSurrogate {Source = testWord});

            return randomPickerMock;
        }
    }
}
