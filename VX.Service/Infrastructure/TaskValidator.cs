using VX.Domain.DataContracts.Interfaces;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure
{
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