using VX.Domain.Interfaces.Entities;

namespace VX.ServiceFacade
{
    public interface IVocabServiceFacade
    {
        ILanguage GetLanguage();

        IWord GerWord();
    }
}
