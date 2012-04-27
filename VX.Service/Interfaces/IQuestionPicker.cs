using VX.Domain.Interfaces.Entities;

namespace VX.Service.Interfaces
{
    public interface IQuestionPicker
    {
        IWord PickQuestion(IVocabBank vocabBank);
    }
}