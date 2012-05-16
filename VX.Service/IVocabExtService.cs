using System.Collections.Generic;
using System.ServiceModel;
using VX.Domain.DataContracts;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Service
{
    [ServiceContract]
    [ServiceKnownType(typeof(TaskContract))]
    public interface IVocabExtService
    {
        [OperationContract]
        ITask GetTask();

        [OperationContract]
        IList<ITask> GetTasks();

        [OperationContract]
        IList<ITask> GetTasks(int[] vocabBanksIds);
    }
}
