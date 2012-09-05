using VX.Domain.Entities;

namespace VX.Domain.Surrogates
{
    public interface IBankTranslationPair
    {
        int VocabBankId { get; }

        ITranslation Translation { get; }
    }
}