using System.IO;
using VX.Domain.Entities.Impl;
using VX.Domain.Surrogates;
using VX.Domain.Surrogates.Impl;
using VX.Model;
using VX.Service.Infrastructure.Factories.Surrogates;

namespace VX.Tests.Mocks
{
    public class SurrogatesFactoryMock : ISurrogatesFactory
    {
        public IBankTranslationPair CreateBankTranslationPair(Stream data)
        {
            return new BankTranslationPair(1, new TranslationContract {Id = 1});
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

        public IVocabBankSummary CreateVocabBankSummary(VocabBank modelData)
        {
            return modelData == null
                       ? null
                       : new VocabBankSummary
                             {
                                 Id = modelData.Id,
                                 Name = modelData.Name,
                                 Description = modelData.Description
                             };
        }

        public IVocabBankSummary CreateVocabBankSummary()
        {
            return new VocabBankSummary();
        }
    }
}
