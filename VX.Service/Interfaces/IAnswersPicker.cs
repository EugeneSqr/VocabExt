using System.Collections.Generic;
using VX.Domain.Interfaces.Entities;

namespace VX.Service.Interfaces
{
    public interface IAnswersPicker
    {
        IList<IWord> PickAnswers(IWord question, IVocabBank vocabBank);
    }
}