using VX.Domain.DataContracts.Interfaces;
using VX.ServiceFacade.VocabServiceReference;

namespace VX.ServiceFacade
{
    public class VocabServiceFacade : IVocabServiceFacade
    {
        private readonly IVocabExtService service = new VocabExtServiceClient();

        public ILanguage GetLanguage()
        {
            return (ILanguage)service.GetLanguage();
        }

        public IWord GerWord()
        {
            return (IWord)service.GetWord();
        }
    }
}
