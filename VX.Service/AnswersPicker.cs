using System.Collections.Generic;
using VX.Domain.Interfaces.Entities;
using VX.Service.Interfaces;

namespace VX.Service
{
    internal class AnswersPicker : IAnswersPicker
    {
        public IList<IWord> PickAnswers(IWord question, IVocabBank vocabBank)
        {
            throw new System.NotImplementedException();
        }
    }
}