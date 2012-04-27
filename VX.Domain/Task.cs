using System.Collections.Generic;
using VX.Domain.Interfaces;
using VX.Domain.Interfaces.Entities;

namespace VX.Domain
{
    public class Task : ITask
    {
        public IWord Question { get; set; }

        public IWord CorrectAnswer { get; set; }

        public IList<IWord> Answers { get; set; }

        public bool IsValidTask()
        {
            return Question != null && CorrectAnswer != null && Answers.Contains(CorrectAnswer);
        }

        public bool IsCorrectAnswer(IWord answer)
        {
            return IsValidTask() && answer.Id == CorrectAnswer.Id;
        }
    }
}