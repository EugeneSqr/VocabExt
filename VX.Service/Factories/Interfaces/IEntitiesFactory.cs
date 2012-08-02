using System.Collections.Generic;
using VX.Domain;
using VX.Domain.DataContracts.Interfaces;
using VX.Model;

namespace VX.Service.Factories.Interfaces
{
    public interface IEntitiesFactory
    {
        ILanguage BuildLanguage(Language language);

        ILanguage BuildLanguage(IDictionary<string, object> language);

        IWord BuildWord(Word word);

        IWord BuildWord(IDictionary<string, object> word);

        ITranslation BuildTranslation(Translation translation);

        ITranslation BuildTranslation(IDictionary<string, object> translation);

        IVocabBank BuildVocabBank(VocabBank vocabBank);

        IVocabBank BuildVocabBankHeaders(IDictionary<string, object> vocabBank);

        IManyToManyRelationship BuildManyToManyRelationship(int id, int sourceId, int targetId);

        ITag BuildTag(Tag tag);
    }
}
