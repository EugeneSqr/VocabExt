using System.Collections.Generic;
using VX.Domain.Interfaces;
using VX.Domain.Interfaces.Entities;

namespace VX.Domain
{
    public class Task : ITask
    {
        public IWord Question { get; set; }

        public IWord Answer { get; set; }

        public IList<IWord> TranslationOptions { get; set; }

        public bool IsValidTask()
        {
            return Question != null && Answer != null && TranslationOptions.Contains(Answer);
        }

        public bool IsCorrectAnswer(IWord answer)
        {
            return IsValidTask() && answer.Id == Answer.Id;
        }
    }
}