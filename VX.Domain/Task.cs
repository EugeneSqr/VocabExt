using System.Collections.Generic;
using System.Runtime.Serialization;
using VX.Domain.Interfaces;
using VX.Domain.Interfaces.Entities;

namespace VX.Domain
{
    [DataContract]
    public class Task : ITask
    {
        [DataMember]
        public IWord Question { get; set; }

        [DataMember]
        public IWord CorrectAnswer { get; set; }

        [DataMember]
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