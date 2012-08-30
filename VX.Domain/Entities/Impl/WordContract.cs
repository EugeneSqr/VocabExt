using System.Runtime.Serialization;

namespace VX.Domain.Entities.Impl
{
    [DataContract]
    [KnownType(typeof(LanguageContract))]
    public class WordContract : IWord
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Spelling { get; set; }

        [DataMember]
        public string Transcription { get; set; }

        [DataMember]
        public ILanguage Language { get; set; }
    }
}
