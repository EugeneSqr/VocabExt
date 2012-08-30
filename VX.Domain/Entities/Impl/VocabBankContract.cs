using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VX.Domain.Entities.Impl
{
    [DataContract]
    [KnownType(typeof(TranslationContract))]
    [KnownType(typeof(TagContract))]
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
