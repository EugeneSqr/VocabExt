using System.Runtime.Serialization;

namespace VX.Domain.Entities.Impl
{
    [DataContract]
    [KnownType(typeof(WordContract))]
    public class TranslationContract : ITranslation
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public IWord Source { get; set; }

        [DataMember]
        public IWord Target { get; set; }
    }
}
