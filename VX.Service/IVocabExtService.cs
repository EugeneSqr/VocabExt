using System.ServiceModel;
using VX.Domain.DataContracts;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Service.Interfaces
{
    [ServiceContract]
    public interface IVocabExtService
    {
        [OperationContract]
        [ServiceKnownType(typeof(LanguageContract))]
        ILanguage GetLanguage();

        [OperationContract]
        [ServiceKnownType(typeof(WordContract))]
        IWord GetWord();
    }
}
