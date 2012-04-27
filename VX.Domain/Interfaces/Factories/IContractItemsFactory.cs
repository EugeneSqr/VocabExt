using VX.Domain.Interfaces.Entities;
using VX.Domain.Surrogates;

namespace VX.Domain.Interfaces.Factories
{
    public interface IContractItemsFactory
    {
        Task BuildTask(ITask task);

        LanguageSurrogate BuildLanguage(ILanguage language);

        WordSurrogate BuildWord(IWord word);
    }
}
