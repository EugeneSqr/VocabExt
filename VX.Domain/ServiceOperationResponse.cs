using System.Runtime.Serialization;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Domain
{
    [DataContract]
    public class ServiceOperationResponse : IServiceOperationResponse
    {
        public ServiceOperationResponse(bool status, string errorMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
        }

        [DataMember]
        public string ErrorMessage { get; private set; }

        [DataMember]
        public bool Status { get; private set; }
    }
}
