using VX.Domain.DataContracts.Interfaces;

namespace VX.Service.Infrastructure.Interfaces
{
    public interface IBankTranslationPair
    {
        int VocabBankId { get; }

        ITranslation Translation { get; }
    }
}