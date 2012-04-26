using VX.Domain.Interfaces;
using VX.Domain.Interfaces.Entities;

namespace VX.Domain.Surrogates
{
    public class LanguageSurrogate : ILanguage
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public bool Equals(LanguageSurrogate other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id == Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof (LanguageSurrogate) && Equals((LanguageSurrogate) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
