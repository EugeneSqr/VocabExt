using System.Collections.Generic;
using VX.Domain.Interfaces.Entities;

namespace VX.Domain.Interfaces
{
    public interface ITask
    {
        IWord Question { get; set; }

        IWord Answer { get; set; }

        IList<IWord> TranslationOptions { get; set; }

        bool IsValidTask();

        bool IsCorrectAnswer(IWord answer);
    }
}
