using System.Collections.Generic;
using System.Runtime.Serialization;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Domain.DataContracts
{
    [DataContract]
    [KnownType(typeof(List<TranslationContract>))]
    [KnownType(typeof(List<TagContract>))]
    public class VocabBankContract : IVocabBank
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public IList<ITranslation> Translations { get; set; }

        [DataMember]
        public IList<ITag> Tags { get; set; }
    }
}
