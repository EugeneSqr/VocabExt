using VX.Domain;
using VX.Domain.Interfaces;
using VX.ServiceFacade.VocabServiceReference;

namespace VX.ServiceFacade
{
    public class VocabServiceFacade : IVocabServiceFacade
    {
        private readonly IVocabExtService service = new VocabExtServiceClient();

        public ILanguage GetLanguage(int languageId)
        {
            return new Language
                       {
                           Name = service.GetData(1)
                       };
        }
    }
}
