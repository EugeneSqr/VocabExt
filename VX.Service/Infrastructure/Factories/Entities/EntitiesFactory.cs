using System;
using System.IO;
using System.Linq;
using VX.Domain.Entities;
using VX.Domain.Entities.Impl;
using VX.Model;
using VX.Service.Infrastructure.Interfaces;

namespace VX.Service.Infrastructure.Factories.Entities
{
    [RegisterService]
    public class EntitiesFactory : AbstractEntitiesFactory, 
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
        private readonly IContractSerializer contractSerializer;

        public EntitiesFactory(IContractSerializer contractSerializer)
        {
            this.contractSerializer = contractSerializer;
        }

        ILanguage ISourceToTargetFactoryMethod<ILanguage, Language>.Create(Language entity)
        {
            Func<Language, ILanguage> mapping =
                item => new LanguageContract
                            {
                                Id = item.Id,
                                Name = item.Name,
                                Abbreviation = item.Abbreviation
                            };

            return Convert(mapping, entity);
        }

        ILanguage ISourceToTargetFactoryMethod<ILanguage, Stream>.Create(Stream data)
        {
            return Parse<ILanguage, LanguageContract>(data);
        }

        IWord ISourceToTargetFactoryMethod<IWord, Word>.Create(Word entity)
        {
            Func<Word, IWord> mapping =
                item => new WordContract
                            {
                                Id = item.Id,
                                Spelling = item.Spelling,
                                Transcription = item.Transcription,
                                Language =
                                    ((ISourceToTargetFactoryMethod<ILanguage, Language>) this).Create(item.Language)
                            };

            return Convert(mapping, entity);
        }

        IWord ISourceToTargetFactoryMethod<IWord, Stream>.Create(Stream data)
        {
            return Parse<IWord, WordContract>(data);
        }

        ITranslation ISourceToTargetFactoryMethod<ITranslation, Translation>.Create(Translation entity)
        {
            Func<Translation, ITranslation> mapping =
                item => new TranslationContract
                            {
                                Id = item.Id,
                                Source = ((ISourceToTargetFactoryMethod<IWord, Word>) this).Create(item.Source),
                                Target = ((ISourceToTargetFactoryMethod<IWord, Word>) this).Create(item.Target)
                            };

            return Convert(mapping, entity);
        }

        ITranslation ISourceToTargetFactoryMethod<ITranslation, Stream>.Create(Stream data)
        {
            return Parse<ITranslation, TranslationContract>(data);
        }

        IVocabBank ISourceToTargetFactoryMethod<IVocabBank, VocabBank>.Create(VocabBank entity)
        {
            Func<VocabBank, IVocabBank> mapping =
                item => new VocabBankContract
                            {
                                Id = item.Id,
                                Name = item.Name,
                                Description = item.Description,
                                Tags = item.VocabBanksTags
                                    .Select(tagItem =>((ISourceToTargetFactoryMethod<ITag, Tag>)this).Create(tagItem.Tag))
                                    .ToList(),
                                Translations = item.VocabBanksTranslations
                                    .Select(
                                        translationItem =>
                                        ((ISourceToTargetFactoryMethod<ITranslation, Translation>) this).Create(
                                            translationItem.Translation))
                                    .ToList()
                            };

            return Convert(mapping, entity);
        }

        ITag ISourceToTargetFactoryMethod<ITag, Tag>.Create(Tag entity)
        {
            Func<Tag, ITag> mapping =
                item => new TagContract
                            {
                                Id = item.Id,
                                Name = item.Name,
                                Description = item.Description
                            };
            return Convert(mapping, entity);
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

        private TType Parse<TType, TContract>(Stream data)
        {
            TType result;
            return contractSerializer.Deserialize<TType, TContract>(data, out result) 
                ? result 
                : Create<TType>();
        }

        private TTarget Convert<TSource, TTarget>(Func<TSource, TTarget> mapping, TSource source) 
            where TSource: class
        {
            return source == null ? Create<TTarget>() : mapping(source);
        }
    }
}