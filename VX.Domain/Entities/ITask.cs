using System.Collections.Generic;

namespace VX.Domain.Entities
{
    public interface ITask
    {
        IWord Question { get; set; }

        IWord CorrectAnswer { get; set; }

        IList<IWord> Answers { get; set; }
    }
}
