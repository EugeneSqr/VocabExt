using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using VX.Domain.DataContracts;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Service
{
    [ServiceContract]
    [ServiceKnownType(typeof(TaskContract))]
    [ServiceKnownType(typeof(VocabBankContract))]
    public interface IVocabExtService
    {
        [OperationContract]
        ITask GetTask();

        [OperationContract(Name="GetTasksAllVocabBanks")]
        IList<ITask> GetTasks();

        [OperationContract(Name="GetTasksSpecifiedVocabBanks")]
        IList<ITask> GetTasks(int[] vocabBanksIds);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IList<IVocabBank> GetVocabBanksList();
    }
}
