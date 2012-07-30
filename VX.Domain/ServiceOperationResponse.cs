using System.Runtime.Serialization;

namespace VX.Domain
{
    [DataContract]
    public class ServiceOperationResponse : IServiceOperationResponse
    {
        public ServiceOperationResponse(bool status)
        {
            Status = status;
        }

        [DataMember]
        public string ErrorMessage { get; set; }

        [DataMember]
        public string StatusMessage { get; set; }

        [DataMember]
        public bool Status { get; private set; }
    }
}
