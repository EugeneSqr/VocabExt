using VX.Domain.Interfaces;

namespace VX.Domain
{
    public class Language : ILanguage
    {
        public string Name { get; set; }

        public string Abbreviation { get; set; }
    }
}
