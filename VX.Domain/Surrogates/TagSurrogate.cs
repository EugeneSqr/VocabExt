using VX.Domain.Interfaces;

namespace VX.Domain.Surrogates
{
    public class TagSurrogate : ITag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
