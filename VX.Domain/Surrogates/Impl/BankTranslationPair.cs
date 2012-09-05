using System.Runtime.Serialization;
using VX.Domain.Entities;
using VX.Domain.Entities.Impl;

namespace VX.Domain.Surrogates.Impl
{
    [DataContract]
    [KnownType(typeof(TranslationContract))]
    public class BankTranslationPair : IBankTranslationPair
    {
        public BankTranslationPair(int vocabBankId, ITranslation translation)
        {
            VocabBankId = vocabBankId;
            Translation = translation;
        }
        
        [DataMember]
        public int VocabBankId { get; private set; }
        
        [DataMember]
        public ITranslation Translation { get; private set; }
    }
}