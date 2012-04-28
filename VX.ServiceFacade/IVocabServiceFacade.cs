using VX.Domain.DataContracts.Interfaces;

namespace VX.ServiceFacade
{
    public interface IVocabServiceFacade
    {
        ILanguage GetLanguage();

        IWord GerWord();
    }
}
