using System.Collections.Generic;
using System.Linq;
using VX.Domain.DataContracts;
using VX.Domain.DataContracts.Interfaces;
using VX.Model;

namespace VX.Service.Infrastructure.Factories.Adapters
{
    public class EntityAdapterFactory : AdapterFactory, 
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
                       : new LanguageContract
                             {
                                 Id = entity.Id,
                                 Name = entity.Name,
                                 Abbreviation = entity.Abbreviation
                             };
        }

        ILanguage IAdapterFactoryMethod<ILanguage, IDictionary<string, object>>.Create(IDictionary<string, object> entity)
        {
            return new LanguageContract
                       {
                           Id = (int) entity["Id"],
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
                           Spelling = entity.Spelling,
                           Transcription = entity.Transcription,
                           Language = ((IAdapterFactoryMethod<ILanguage, Language>)this).Create(entity.Language)
                       };
        }

        IWord IAdapterFactoryMethod<IWord, IDictionary<string, object>>.Create(IDictionary<string, object> entity)
        {
            const string idKey = "Id";
            const string spellingKey = "Spelling";
            const string transciptionKey = "Transcription";
            const string languageKey = "Language";

            return new WordContract
            {
                Id = entity.ContainsKey(idKey)
                    ? (int)entity[idKey]
                    : -1,
                Spelling = entity.ContainsKey(spellingKey)
                    ? entity[spellingKey].ToString()
                    : null,
                Transcription = entity.ContainsKey(transciptionKey)
                    ? entity[transciptionKey].ToString()
                    : null,
                Language = entity.ContainsKey(languageKey)
                    ? ((IAdapterFactoryMethod<ILanguage, IDictionary<string, object>>)this).Create((IDictionary<string, object>)entity[languageKey])
                    : null
            };
        }

        ITranslation IAdapterFactoryMethod<ITranslation, Translation>.Create(Translation entity)
        {
            return entity == null
                       ? null
                       : new TranslationContract
                             {
                                 Id = entity.Id,
                                 Source = ((IAdapterFactoryMethod<IWord, Word>)this).Create(entity.Source),
                                 Target = ((IAdapterFactoryMethod<IWord, Word>)this).Create(entity.Target)
                             };
        }

        ITranslation IAdapterFactoryMethod<ITranslation, IDictionary<string, object>>.Create(IDictionary<string, object> entity)
        {
            return new TranslationContract
            {
                Id = (int)entity["Id"],
                Source = ((IAdapterFactoryMethod<IWord, IDictionary<string, object>>)this).Create((IDictionary<string, object>)entity["Source"]),
                Target = ((IAdapterFactoryMethod<IWord, IDictionary<string, object>>)this).Create((IDictionary<string, object>)entity["Target"])
            };
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
                           Tags = entity.VocabBanksTags
                              .Select(item => ((IAdapterFactoryMethod<ITag, Tag>)this).Create(item.Tag))
                              .ToList(),
                           Translations = entity.VocabBanksTranslations
                              .Select(item => ((IAdapterFactoryMethod<ITranslation, Translation>)this).Create(item.Translation))
                              .ToList()
                       };
        }

        IVocabBank IAdapterFactoryMethod<IVocabBank, IDictionary<string, object>>.Create(IDictionary<string, object> entity)
        {
            return new VocabBankContract
            {
                Id = (int)entity["VocabBankId"],
                Name = entity["Name"].ToString(),
                Description = entity["Description"].ToString()
            };
        }

        ITag IAdapterFactoryMethod<ITag, Tag>.Create(Tag entity)
        {
            return entity == null
                       ? null
                       : new TagContract
                       {
                           Id = entity.Id,
                           Name = entity.Name,
                           Description = entity.Description
                       };
        }
    }
}