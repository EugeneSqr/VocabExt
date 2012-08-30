using System.Runtime.Serialization;

namespace VX.Domain.Entities.Impl
{
    [DataContract]
    public class LanguageContract : ILanguage
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Abbreviation { get; set; }
    }
}
