using System.Collections.Generic;
using VX.Domain.Interfaces.Entities;

namespace VX.Service
{
    public interface IQuestionPicker
    {
        IWord PickQuestion(IVocabBank vocabBank);

        IWord PickQuestion(IList<IVocabBank> vocabBanks);
    }
}