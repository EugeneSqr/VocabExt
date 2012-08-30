using System.Runtime.Serialization;

namespace VX.Domain.Entities.Impl
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
