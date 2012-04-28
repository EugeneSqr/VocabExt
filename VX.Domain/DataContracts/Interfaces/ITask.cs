using System.Collections.Generic;

namespace VX.Domain.DataContracts.Interfaces
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
