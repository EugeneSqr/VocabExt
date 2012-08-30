using VX.Domain.Entities;

namespace VX.Service.Infrastructure.Interfaces
{
    public interface IBankTranslationPair
    {
        int VocabBankId { get; }

        ITranslation Translation { get; }
    }
}