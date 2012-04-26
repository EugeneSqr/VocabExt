using VX.Domain.Interfaces;
using VX.Domain.Interfaces.Entities;

namespace VX.Domain.Surrogates
{
    public class TagSurrogate : ITag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
