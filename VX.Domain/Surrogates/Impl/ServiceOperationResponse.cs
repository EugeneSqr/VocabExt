using System.Runtime.Serialization;

namespace VX.Domain.Surrogates.Impl
{
    [DataContract]
    public class ServiceOperationResponse : IServiceOperationResponse
    {
        public ServiceOperationResponse(bool status, ServiceOperationAction action)
        {
            Status = status;
            OperationActionCode = (int)action;
        }

        [DataMember]
        public string ErrorMessage { get; set; }

        [DataMember]
        public string StatusMessage { get; set; }

        [DataMember]
        public int OperationActionCode { get; private set; }

        [DataMember]
        public bool Status { get; private set; }
    }
}
