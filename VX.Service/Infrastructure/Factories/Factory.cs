using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using VX.Domain.Entities;
using VX.Domain.Entities.Impl;
using VX.Domain.Surrogates;
using VX.Domain.Surrogates.Impl;
using VX.Model;

namespace VX.Service.Infrastructure.Factories
{
    [RegisterService]
    public class Factory : AbstractFactory, 
        ISourceToTargetFactoryMethod<ILanguage, Language>,
        ISourceToTargetFactoryMethod<ILanguage, Stream>,
        ISourceToTargetFactoryMethod<IWord, Word>,
        ISourceToTargetFactoryMethod<IWord, Stream>,
        ISourceToTargetFactoryMethod<ITranslation, Translation>,
        ISourceToTargetFactoryMethod<ITranslation, Stream>,
        ISourceToTargetFactoryMethod<IVocabBank, VocabBank>,
        ISourceToTargetFactoryMethod<IVocabBankSummary, Stream>,
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

        IVocabBankSummary ISourceToTargetFactoryMethod<IVocabBankSummary, Stream>.Create(Stream source)
        {
            return Parse<IVocabBankSummary, VocabBankSummary>(source);
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

        private TType Parse<TType, TContract>(Stream data)
        {
            try
            {
                return (TType)new DataContractJsonSerializer(typeof(TContract)).ReadObject(data);
            }
            catch (SerializationException)
            {
                return Create<TType>();
            }
            catch(InvalidCastException)
            {
                return Create<TType>();
            }
        }

        private TTarget Convert<TSource, TTarget>(Func<TSource, TTarget> mapping, TSource source) 
            where TSource: class
        {
            return source == null ? Create<TTarget>() : mapping(source);
        }

        public IServiceOperationResponse Create(bool status, ServiceOperationAction action)
        {
            return new ServiceOperationResponse(status, action);
        }

        public IServiceOperationResponse Create(bool status, ServiceOperationAction action, string message)
        {
            var result = Create(status, action);
            if (status)
            {
                result.StatusMessage = message;
            }
            else
            {
                result.ErrorMessage = message;
            }

            return result;
        }
    }
}