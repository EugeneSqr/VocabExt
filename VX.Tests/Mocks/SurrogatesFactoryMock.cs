using System.IO;
using VX.Domain.Surrogates;
using VX.Domain.Surrogates.Impl;
using VX.Service.Infrastructure.Factories.Surrogates;

namespace VX.Tests.Mocks
{
    public class SurrogatesFactoryMock : ISurrogatesFactory
    {
        public IBankTranslationPair CreateBankTranslationPair(Stream data)
        {
            throw new System.NotImplementedException();
        }

        public IBankTranslationPair CreateBankTranslationPair()
        {
            throw new System.NotImplementedException();
        }

        public IParentChildIdPair CreateParentChildIdPair(Stream data)
        {
            throw new System.NotImplementedException();
        }

        public IParentChildIdPair CreateParentChildIdPair()
        {
            throw new System.NotImplementedException();
        }

        public IManyToManyRelationship CreateManyToManyRelationship(int relationshipId, int firstId, int secondId)
        {
            throw new System.NotImplementedException();
        }

        public IVocabBankSummary CreateVocabBankSummary(Stream data)
        {
            return new VocabBankSummary();
        }

        public IVocabBankSummary CreateVocabBankSummary()
        {
            return new VocabBankSummary();
        }
    }
}
