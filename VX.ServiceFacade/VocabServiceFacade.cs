
using VX.Domain.Interfaces.Entities;
using VX.ServiceFacade.VocabServiceReference;

namespace VX.ServiceFacade
{
    public class VocabServiceFacade : IVocabServiceFacade
    {
        private readonly IVocabExtService service = new VocabExtServiceClient();

        public ILanguage GetLanguage()
        {
            return service.GetLanguage();
        }

        public IWord GerWord()
        {
            return service.GetWord();
        }
    }
}
