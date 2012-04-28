using System.Runtime.Serialization;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Domain.DataContracts
{
    [DataContract]
    public class TagContract : ITag
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
