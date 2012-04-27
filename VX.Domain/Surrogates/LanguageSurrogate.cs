using System.Runtime.Serialization;
using VX.Domain.Interfaces.Entities;

namespace VX.Domain.Surrogates
{
    [DataContract]
    public class LanguageSurrogate : ILanguage
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
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
