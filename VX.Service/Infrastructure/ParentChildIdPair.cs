using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure
{
    public class ParentChildIdPair : IParentChildIdPair
    {
        public ParentChildIdPair(int parentId, int childId)
        {
            ParentId = parentId;

            ChildId = childId;
        }

        public int ParentId { get; private set; }

        public int ChildId { get; private set; }
    }
}