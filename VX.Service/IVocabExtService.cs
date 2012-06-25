using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using VX.Domain.DataContracts;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Service
{
    [ServiceContract]
    [ServiceKnownType(typeof(TaskContract))]
    [ServiceKnownType(typeof(VocabBankContract))]
    [ServiceKnownType(typeof(TranslationContract))]
    [ServiceKnownType(typeof(WordContract))]
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

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetTranslations/{vocabBankId}")]
        IList<ITranslation> GetTranslations(string vocabBankId);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetWords/{searchString}")]
        IList<IWord> GetWords(string searchString);

        [OperationContract]
        [WebInvoke(Method = "POST")]
        bool UpdateTranslation(Stream data);
    }
}
