using System.Collections.Generic;
using VX.Domain.Interfaces.Entities;

namespace VX.Domain.Interfaces
{
    public interface ITask
    {
        IWord Question { get; set; }

        IWord CorrectAnswer { get; set; }

        IList<IWord> Answers { get; set; }

        bool IsValidTask();

        bool IsCorrectAnswer(IWord answer);
    }
}
