using VX.Domain.Entities;

namespace VX.Service.Infrastructure.Interfaces
{
    public interface ITaskValidator
    {
        bool IsValidTask(ITask task);
    }
}