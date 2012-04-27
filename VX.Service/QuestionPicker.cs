using System;
using VX.Domain.Interfaces.Entities;
using VX.Service.Interfaces;

namespace VX.Service
{
    public class QuestionPicker : IQuestionPicker
    {
        private readonly IRandomPicker randomPicker;
        
        public QuestionPicker(IRandomPicker randomPicker)
        {
            this.randomPicker = randomPicker;
        }

        public IWord PickQuestion(IVocabBank vocabBank)
        {
            if (vocabBank == null || vocabBank.Translations == null || vocabBank.Translations.Count == 0)
            {
                // TODO: licalize
                throw new ArgumentNullException("vocabBank", "Input VocabBank is empty");
            }

            var translation = randomPicker.PickItem(vocabBank.Translations);
            return translation.Source;
        }
    }
}