using VX.Domain.Interfaces;

namespace VX.Domain
{
    public class Language : ILanguage
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public bool Equals(Language other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id == Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof (Language) && Equals((Language) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
