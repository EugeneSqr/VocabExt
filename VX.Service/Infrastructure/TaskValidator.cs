using VX.Domain.Entities;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure
{
    [RegisterService]
    public class TaskValidator : ITaskValidator
    {
        public bool IsValidTask(ITask task)
        {
            return task.Question != null 
                && task.CorrectAnswer != null 
                && task.Answers.Contains(task.CorrectAnswer);
        }
    }
}