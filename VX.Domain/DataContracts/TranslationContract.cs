using System.Runtime.Serialization;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Domain.DataContracts
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
