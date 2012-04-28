using System.ServiceModel;
using VX.Domain.DataContracts;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Service
{
    [ServiceContract]
    public interface IVocabExtService
    {
        [OperationContract]
        [ServiceKnownType(typeof(TaskContract))]
        ITask GetTask();
    }
}
