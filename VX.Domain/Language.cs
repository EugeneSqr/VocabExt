using VX.Domain.Interfaces;

namespace VX.Domain
{
    public class Language : ILanguage
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Abbreviation { get; set; }
    }
}
