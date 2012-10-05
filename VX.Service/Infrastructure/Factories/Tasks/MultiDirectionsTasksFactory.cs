using System;
using System.Linq;
using VX.Domain.Entities;
using VX.Domain.Entities.Impl;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure.Factories.Tasks
{
    [RegisterService]
    public class MultiDirectionsTasksFactory : TasksFactoryBase
    {
        public MultiDirectionsTasksFactory(IRandomPicker randomPicker, ITaskValidator taskValidator, ISynonymSelector synonymSelector) : base(randomPicker, taskValidator, synonymSelector)
        {
        }

        public override ITask BuildTask(IVocabBank vocabBank)
        {
            var questionTranslation = RandomPicker.PickItem(vocabBank.Translations);
            bool isReverse = IsReverseDirection();
            IWord question;
            IWord correctAnswer;
            Func<ITranslation, IWord> answersPicker;
            if (isReverse)
            {
                question = questionTranslation.Target;
                correctAnswer = questionTranslation.Source;
                answersPicker = translation => translation.Source;
            }
            else
            {
                question = questionTranslation.Source;
                correctAnswer = questionTranslation.Target;
                answersPicker = translation => translation.Target;
            }

            var questionBlacklist = SynonymSelector.GetSimilarTranslations(questionTranslation, vocabBank.Translations);
            var answers = RandomPicker
                .PickItems(vocabBank.Translations, DefaultAnswersCount, questionBlacklist)
                .Select(answersPicker)
                .ToList();

            answers.Insert(RandomPicker.PickInsertIndex(answers), correctAnswer);

            var result = new TaskContract
            {
                Answers = answers,
                CorrectAnswer = correctAnswer,
                Question = question
            };

            if (TaskValidator.IsValidTask(result))
            {
                return result;
            }

            throw new ArgumentException("Task created incorrectly.", "vocabBank");
        }

        private bool IsReverseDirection()
        {
            return RandomPicker.PickItem(new [] {0, 1}) == 0;
        }
    }
}