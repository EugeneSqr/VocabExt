using System;
using System.Collections.Generic;
using VX.Domain.Interfaces.Entities;
using VX.Service.Interfaces;

namespace VX.Service
{
    internal class QuestionPicker : IQuestionPicker
    {
        private readonly IRandomFacade randomFacade;
        
        public QuestionPicker(IRandomFacade randomFacade)
        {
            this.randomFacade = randomFacade;
        }

        public IWord PickQuestion(IVocabBank vocabBank)
        {
            if (vocabBank == null)
            {
                // TODO: licalize
                throw new ArgumentNullException("vocabBank", "Input VocabBank is empty");
            }

            var translation = PickOneFromList(vocabBank.Translations);
            return translation.Source;
        }

        public IWord PickQuestion(IList<IVocabBank> vocabBanks)
        {
            var vocabBank = PickOneFromList(vocabBanks);
            return PickQuestion(vocabBank);
        }

        private T PickOneFromList<T>(IList<T> list)
        {
            if (list == null || list.Count == 0)
            {
                // TODO: localize
                throw new ArgumentNullException(
                    "list", 
                    string.Format("Can't pick an item from empty list. Type: {0}", typeof(T)));
            }

            int position = randomFacade.PickRandomValue(0, list.Count);
            return list[position];
        }
    }
}