using System.Collections.Generic;
using VX.Domain.DataContracts;
using VX.Domain.DataContracts.Interfaces;
using VX.Model;
using VX.Service.Infrastructure.Factories.Adapters;

namespace VX.Tests.Mocks
{
    public class EntitiesFactoryMock : AdapterFactory, 
        IAdapterFactoryMethod<ILanguage, Language>,
        IAdapterFactoryMethod<ILanguage, IDictionary<string, object>>,
        IAdapterFactoryMethod<IWord, Word>,
        IAdapterFactoryMethod<IWord, IDictionary<string, object>>,
        IAdapterFactoryMethod<ITranslation, Translation>,
        IAdapterFactoryMethod<ITranslation, IDictionary<string, object>>,
        IAdapterFactoryMethod<IVocabBank, VocabBank>,
        IAdapterFactoryMethod<IVocabBank, IDictionary<string, object>>,
        IAdapterFactoryMethod<ITag, Tag>
    {
        ILanguage IAdapterFactoryMethod<ILanguage, Language>.Create(Language entity)
        {
            return entity == null
                       ? null
                       : new LanguageContract();
        }

        ILanguage IAdapterFactoryMethod<ILanguage, IDictionary<string, object>>.Create(IDictionary<string, object> entity)
        {
            return new LanguageContract
            {
                Id = (int)entity["Id"],
                Name = entity["Name"].ToString(),
                Abbreviation = entity["Abbreviation"].ToString()
            };
            
            
        }

        IWord IAdapterFactoryMethod<IWord, Word>.Create(Word entity)
        {
            return entity == null
                       ? null
                       : new WordContract
                       {
                           Id = entity.Id,
                           Spelling = entity.Spelling
                       };
        }

        IWord IAdapterFactoryMethod<IWord, IDictionary<string, object>>.Create(IDictionary<string, object> entity)
        {
            return new WordContract
            {
                Id = (int)entity["Id"],
                Spelling = entity["Spelling"].ToString(),
                Transcription = entity["Transcription"].ToString(),
                Language = Create<ILanguage, IDictionary<string, object>>((IDictionary<string, object>)entity["Language"])
            };
        }

        ITranslation IAdapterFactoryMethod<ITranslation, Translation>.Create(Translation entity)
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

        ITranslation IAdapterFactoryMethod<ITranslation, IDictionary<string, object>>.Create(IDictionary<string, object> entity)
        {
            throw new System.NotImplementedException();
        }

        IVocabBank IAdapterFactoryMethod<IVocabBank, VocabBank>.Create(VocabBank entity)
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

        IVocabBank IAdapterFactoryMethod<IVocabBank, IDictionary<string, object>>.Create(IDictionary<string, object> entity)
        {
            return new VocabBankContract
            {
                Name = entity["Name"].ToString(),
                Description = entity["Description"].ToString()
            };
        }

        ITag IAdapterFactoryMethod<ITag, Tag>.Create(Tag entity)
        {
            throw new System.NotImplementedException();
        }
    }
}