using System.Runtime.Serialization;
using VX.Domain.Interfaces.Entities;

namespace VX.Domain.Surrogates
{
    [DataContract]
    public class WordSurrogate : IWord
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
