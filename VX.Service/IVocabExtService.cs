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

        [OperationContract(Name="GetTasksAllVocabBanks")]
        IList<ITask> GetTasks();

        [OperationContract(Name="GetTasksSpecifiedVocabBanks")]
        IList<ITask> GetTasks(int[] vocabBanksIds);

        [OperationContract]
        IList<IVocabBank> GetVocabBanksList();
    }
}
