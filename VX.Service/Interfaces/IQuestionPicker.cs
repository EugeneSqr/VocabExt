using System.Collections.Generic;
using VX.Domain.Interfaces.Entities;

namespace VX.Service.Interfaces
{
    internal interface IQuestionPicker
    {
        IWord PickQuestion(IVocabBank vocabBank);

        IWord PickQuestion(IList<IVocabBank> vocabBanks);
    }
}