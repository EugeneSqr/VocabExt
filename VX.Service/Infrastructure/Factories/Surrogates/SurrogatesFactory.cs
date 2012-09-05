using System;
using System.IO;
using VX.Domain.Entities;
using VX.Domain.Surrogates;
using VX.Domain.Surrogates.Impl;
using VX.Service.Infrastructure.Factories.Entities;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure.Factories.Surrogates
{
    [RegisterService]
    public class SurrogatesFactory : ISurrogatesFactory
    {
        private readonly IContractSerializer contractSerializer;
        private readonly IAbstractEntitiesFactory entitiesFactory;

        public SurrogatesFactory(IContractSerializer contractSerializer, IAbstractEntitiesFactory entitiesFactory)
        {
            this.contractSerializer = contractSerializer;
            this.entitiesFactory = entitiesFactory;
        }

        public IBankTranslationPair CreateBankTranslationPair(Stream data)
        {
            return Create<IBankTranslationPair, BankTranslationPair>(data, CreateBankTranslationPair);
        }

        public IBankTranslationPair CreateBankTranslationPair()
        {
            return new BankTranslationPair(default(int), entitiesFactory.Create<ITranslation>());
        }

        public IParentChildIdPair CreateParentChildIdPair(Stream data)
        {
            return Create<IParentChildIdPair, ParentChildIdPair>(data, CreateParentChildIdPair);
        }

        public IParentChildIdPair CreateParentChildIdPair()
        {
            return new ParentChildIdPair(default(int), default(int));
        }

        public IManyToManyRelationship CreateManyToManyRelationship(
            int relationshipId, 
            int firstId, 
            int secondId)
        {
            throw new NotImplementedException();
        }

        public IVocabBankSummary CreateVocabBankSummary(Stream data)
        {
            return Create<IVocabBankSummary, VocabBankSummary>(data, CreateVocabBankSummary);
        }

        public IVocabBankSummary CreateVocabBankSummary()
        {
            return new VocabBankSummary();
        }

        private TType Create<TType, TContract>(Stream data, Func<TType> fallbackFuntion)
        {
            TType result;
            return contractSerializer.Deserialize<TType, TContract>(data, out result)
                ? result
                : fallbackFuntion();
        }
    }
}