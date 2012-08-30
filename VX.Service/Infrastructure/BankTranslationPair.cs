using VX.Domain.Entities;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure
{
    public class BankTranslationPair : IBankTranslationPair
    {
        public BankTranslationPair()
        {
        }

        public BankTranslationPair(int vocabBankId, ITranslation translation)
        {
            VocabBankId = vocabBankId;
            Translation = translation;
        }

        public int VocabBankId { get; private set; }

        public ITranslation Translation { get; private set; }
    }
}