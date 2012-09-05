using System.Runtime.Serialization;

namespace VX.Domain.Surrogates.Impl
{
    [DataContract]
    public class ParentChildIdPair : IParentChildIdPair
    {
        public ParentChildIdPair(int parentId, int childId)
        {
            ParentId = parentId;

            ChildId = childId;
        }

        [DataMember]
        public int ParentId { get; private set; }

        [DataMember]
        public int ChildId { get; private set; }
    }
}