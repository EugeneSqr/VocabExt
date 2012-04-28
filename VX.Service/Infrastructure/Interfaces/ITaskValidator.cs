using VX.Domain.DataContracts.Interfaces;

namespace VX.Service.Infrastructure.Interfaces
{
    public interface ITaskValidator
    {
        bool IsValidTask(ITask task);
    }
}