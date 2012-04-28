using VX.Domain.DataContracts.Interfaces;

namespace VX.Domain.DataContracts
{
    public class TagContract : ITag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
