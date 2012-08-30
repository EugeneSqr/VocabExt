using System.Collections.Generic;
using VX.Domain.Entities;
using VX.Domain.Entities.Impl;
using VX.Domain.Surrogates;
using VX.Domain.Surrogates.Impl;
using VX.Model;
using VX.Service.Infrastructure.Factories;

namespace VX.Tests.Mocks
{
    public class FactoryMock : AbstractFactory, 
        ISourceToTargetFactoryMethod<ILanguage, Language>,
        ISourceToTargetFactoryMethod<ILanguage, IDictionary<string, object>>,
        ISourceToTargetFactoryMethod<IWord, Word>,
        ISourceToTargetFactoryMethod<IWord, IDictionary<string, object>>,
        ISourceToTargetFactoryMethod<ITranslation, Translation>,
        ISourceToTargetFactoryMethod<ITranslation, IDictionary<string, object>>,
        ISourceToTargetFactoryMethod<IVocabBank, VocabBank>,
        ISourceToTargetFactoryMethod<IVocabBank, IDictionary<string, object>>,
        ISourceToTargetFactoryMethod<ITag, Tag>,
        IFactoryMethod<ILanguage>,
        IFactoryMethod<IWord>,
        IFactoryMethod<ITranslation>,
        IFactoryMethod<IVocabBankSummary>,
        IFactoryMethod<IVocabBank>,
        IFactoryMethod<ITag>,
        IResponseFactoryMethod<IServiceOperationResponse>
    {
        ILanguage ISourceToTargetFactoryMethod<ILanguage, Language>.Create(Language entity)
        {
            return entity == null
                       ? null
                       : new LanguageContract();
        }

        ILanguage ISourceToTargetFactoryMethod<ILanguage, IDictionary<string, object>>.Create(IDictionary<string, object> entity)
        {
            return new LanguageContract
            {
                Id = (int)entity["Id"],
                Name = entity["Name"].ToString(),
                Abbreviation = entity["Abbreviation"].ToString()
            };
            
            
        }

        IWord ISourceToTargetFactoryMethod<IWord, Word>.Create(Word entity)
        {
            return entity == null
                       ? null
                       : new WordContract
                       {
                           Id = entity.Id,
                           Spelling = entity.Spelling
                       };
        }

        IWord ISourceToTargetFactoryMethod<IWord, IDictionary<string, object>>.Create(IDictionary<string, object> entity)
        {
            return new WordContract
            {
                Id = (int)entity["Id"],
                Spelling = entity["Spelling"].ToString(),
                Transcription = entity["Transcription"].ToString(),
                Language = Create<ILanguage, IDictionary<string, object>>((IDictionary<string, object>)entity["Language"])
            };
        }

        ITranslation ISourceToTargetFactoryMethod<ITranslation, Translation>.Create(Translation entity)
        {
            return entity == null
                       ? null
                       : new TranslationContract
                       {
                           Id = entity.Id,
                           Source = new WordContract
                           {
                               Id = entity.Source.Id
                           },
                           Target = new WordContract
                           {
                               Id = entity.Target.Id
                           }
                       };
        }

        ITranslation ISourceToTargetFactoryMethod<ITranslation, IDictionary<string, object>>.Create(IDictionary<string, object> entity)
        {
            throw new System.NotImplementedException();
        }

        IVocabBank ISourceToTargetFactoryMethod<IVocabBank, VocabBank>.Create(VocabBank entity)
        {
            return entity == null
                       ? null
                       : new VocabBankContract
                       {
                           Id = entity.Id,
                           Name = entity.Name,
                           Description = entity.Description,
                           Tags =
                               entity.VocabBanksTags.Count == 0
                                   ? new List<ITag>()
                                   : new List<ITag> { new TagContract() },
                           Translations =
                               entity.VocabBanksTranslations.Count == 0
                                   ? new List<ITranslation>()
                                   : new List<ITranslation> { new TranslationContract() }
                       };
        }

        IVocabBank ISourceToTargetFactoryMethod<IVocabBank, IDictionary<string, object>>.Create(IDictionary<string, object> entity)
        {
            return new VocabBankContract
            {
                Name = entity["Name"].ToString(),
                Description = entity["Description"].ToString()
            };
        }

        ITag ISourceToTargetFactoryMethod<ITag, Tag>.Create(Tag entity)
        {
            throw new System.NotImplementedException();
        }

        ILanguage IFactoryMethod<ILanguage>.Create()
        {
            return new LanguageContract();
        }

        IWord IFactoryMethod<IWord>.Create()
        {
            return new WordContract();
        }

        ITranslation IFactoryMethod<ITranslation>.Create()
        {
            return new TranslationContract();
        }

        IVocabBankSummary IFactoryMethod<IVocabBankSummary>.Create()
        {
            return new VocabBankSummary();
        }

        IVocabBank IFactoryMethod<IVocabBank>.Create()
        {
            return new VocabBankContract();
        }

        ITag IFactoryMethod<ITag>.Create()
        {
            return new TagContract();
        }

        IServiceOperationResponse IResponseFactoryMethod<IServiceOperationResponse>.Create(
            bool status, 
            ServiceOperationAction action)
        {
            return new ServiceOperationResponse(status, action);
        }

        public IServiceOperationResponse Create(bool status, ServiceOperationAction action, string message)
        {
            return status 
                    ? new ServiceOperationResponse(true, action) {StatusMessage = message} 
                    : new ServiceOperationResponse(false, action) {ErrorMessage = message};
        }
    }
}