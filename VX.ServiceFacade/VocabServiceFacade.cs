using VX.Domain.Interfaces;
using VX.Domain.Interfaces.Entities;
using VX.Domain.Surrogates;
using VX.ServiceFacade.VocabServiceReference;

namespace VX.ServiceFacade
{
    public class VocabServiceFacade : IVocabServiceFacade
    {
        private readonly IVocabExtService service = new VocabExtServiceClient();

        public ILanguage GetLanguage(int languageId)
        {
            return new LanguageSurrogate
                       {
                           Name = service.GetData(1)
                       };
            
        }
    }
}
