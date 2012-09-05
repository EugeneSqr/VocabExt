using System.Collections.Generic;
using System.IO;
using VX.Domain.Entities;
using VX.Domain.Entities.Impl;
using VX.Model;
using VX.Service.Infrastructure.Factories;
using VX.Service.Infrastructure.Factories.Entities;

namespace VX.Tests.Mocks
{
    public class EntitiesFactoryMock : AbstractEntitiesFactory,
        ISourceToTargetFactoryMethod<ILanguage, Language>,
        ISourceToTargetFactoryMethod<ILanguage, Stream>,
        ISourceToTargetFactoryMethod<IWord, Word>,
        ISourceToTargetFactoryMethod<IWord, Stream>,
        ISourceToTargetFactoryMethod<ITranslation, Translation>,
        ISourceToTargetFactoryMethod<ITranslation, Stream>,
        ISourceToTargetFactoryMethod<IVocabBank, VocabBank>,
        ISourceToTargetFactoryMethod<ITag, Tag>,
        IDefaultFactoryMethod<ILanguage>,
        IDefaultFactoryMethod<IWord>,
        IDefaultFactoryMethod<ITranslation>,
        IDefaultFactoryMethod<IVocabBank>,
        IDefaultFactoryMethod<ITag>
    {
        ILanguage ISourceToTargetFactoryMethod<ILanguage, Language>.Create(Language entity)
        {
            return entity == null
                       ? null
                       : new LanguageContract();
        }

        ILanguage ISourceToTargetFactoryMethod<ILanguage, Stream>.Create(Stream source)
        {
            throw new System.NotImplementedException();
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

        IWord ISourceToTargetFactoryMethod<IWord, Stream>.Create(Stream source)
        {
            throw new System.NotImplementedException();
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

        ITranslation ISourceToTargetFactoryMethod<ITranslation, Stream>.Create(Stream source)
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

        

        ITag ISourceToTargetFactoryMethod<ITag, Tag>.Create(Tag entity)
        {
            throw new System.NotImplementedException();
        }

        ILanguage IDefaultFactoryMethod<ILanguage>.Create()
        {
            return new LanguageContract();
        }

        IWord IDefaultFactoryMethod<IWord>.Create()
        {
            return new WordContract();
        }

        ITranslation IDefaultFactoryMethod<ITranslation>.Create()
        {
            return new TranslationContract();
        }

        IVocabBank IDefaultFactoryMethod<IVocabBank>.Create()
        {
            return new VocabBankContract();
        }

        ITag IDefaultFactoryMethod<ITag>.Create()
        {
            return new TagContract();
        }
    }
}