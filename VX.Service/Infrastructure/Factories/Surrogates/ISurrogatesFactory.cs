using System.IO;
using VX.Domain.Surrogates;
using VX.Model;

namespace VX.Service.Infrastructure.Factories.Surrogates
{
    public interface ISurrogatesFactory
    {
        IBankTranslationPair CreateBankTranslationPair(Stream data);

        IBankTranslationPair CreateBankTranslationPair();

        IParentChildIdPair CreateParentChildIdPair(Stream data);

        IParentChildIdPair CreateParentChildIdPair();

        IVocabBankSummary CreateVocabBankSummary(Stream data);

        IVocabBankSummary CreateVocabBankSummary(VocabBank modelData);

        IVocabBankSummary CreateVocabBankSummary();

        IManyToManyRelationship CreateManyToManyRelationship(
            int relationshipId,
            int firstId,
            int secondId);
    }
}