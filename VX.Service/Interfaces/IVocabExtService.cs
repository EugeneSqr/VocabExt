using System.ServiceModel;
using VX.Domain.Surrogates;

namespace VX.Service.Interfaces
{
    [ServiceContract]
    public interface IVocabExtService
    {
        [OperationContract]
        LanguageSurrogate GetLanguage();

        [OperationContract]
        WordSurrogate GetWord();
    }
}
