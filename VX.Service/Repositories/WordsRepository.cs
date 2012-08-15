using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using VX.Domain;
using VX.Domain.DataContracts.Interfaces;
using VX.Model;
using VX.Service.Factories.Interfaces;
using VX.Service.Infrastructure.Interfaces;
using VX.Service.Repositories.Interfaces;
using VX.Service.Validators.Interfaces;

namespace VX.Service.Repositories
{
    public class WordsRepository : RepositoryBase, IWordsRepository
    {
        private readonly ISearchStringBuilder searchStringBuilder;
        private readonly IWordValidator wordValidator;

        public WordsRepository(
            IContextFactory contextFactory, 
            IEntitiesFactory entitiesFactory, 
            ICacheFacade cacheFacade, 
            ICacheKeyFactory cacheKeyFactory, 
            IServiceOperationResponseFactory serviceOperationResponseFactory,
            ISearchStringBuilder searchStringBuilder,
            IWordValidator wordValidator) : base(contextFactory, entitiesFactory, cacheFacade, cacheKeyFactory, serviceOperationResponseFactory)
        {
            this.searchStringBuilder = searchStringBuilder;
            this.wordValidator = wordValidator;
        }

        public IList<IWord> GetWords(string searchString)
        {
            List<IWord> result;
            var cacheKey = CacheKeyFactory.BuildKey("WordsRepository.GetWords", searchString);
            if (!CacheFacade.GetFromCache(cacheKey, out result))
            {
                var actualSearchString = searchStringBuilder.BuildSearchString(searchString);
                if (!string.IsNullOrEmpty(actualSearchString))
                {
                    using (Entities context = ContextFactory.Build())
                    {
                        result = context.Words
                            .Where(word => word.Spelling.StartsWith(actualSearchString))
                            .ToList()
                            .Select(item => EntitiesFactory.BuildWord(item))
                            .ToList();
                    }
                }
                else
                {
                    result = new List<IWord>();
                }

                CacheFacade.PutIntoCache(result, cacheKey);
            }
            
            return result;
        }

        public IWord GetWord(int id)
        {
            var cacheKey = CacheKeyFactory.BuildKey("WordsRepository.GetWord", id);
            Func<Entities, int, IWord> retrievalFunction = (context, parameter) => EntitiesFactory.BuildWord(
                context.Words.FirstOrDefault(item => item.Id == id));

            return Retrieve(retrievalFunction, cacheKey, id);
        }

        public bool CheckWordExists(string spelling)
        {
            var cacheKey = CacheKeyFactory.BuildKey("WordsRepository.CheckWord", spelling);
            Func<Entities, string, bool> retrievalFunction = (context, parameter) => 
                context.Words.FirstOrDefault(item => item.Spelling == spelling) != null;
            
            return Retrieve(retrievalFunction, cacheKey, spelling);
        }

        public IServiceOperationResponse SaveWord(IWord word)
        {
            var validationResult = wordValidator.Validate(word);
            if (!validationResult.Status)
            {
                return validationResult;
            }

            using (Entities context = ContextFactory.Build())
            {
                var newWord = context.Words.CreateObject();
                newWord.LanguageId = word.Language.Id;
                newWord.Spelling = word.Spelling;
                newWord.Transcription = word.Transcription;
                context.Words.AddObject(newWord);
                context.SaveChanges();
                return ServiceOperationResponseFactory.Build(
                    true, 
                    ServiceOperationAction.Create, 
                    newWord.Id.ToString(CultureInfo.InvariantCulture));
            }
        }

        private TResult Retrieve<TParameter, TResult>(Func<Entities, TParameter, TResult> retrievalFunction, string cacheKey, TParameter parameter)
        {
            TResult result;
            if (!CacheFacade.GetFromCache(cacheKey, out result))
            {
                using (var context = ContextFactory.Build())
                {
                    result = retrievalFunction(context, parameter);
                }

                CacheFacade.PutIntoCache(result, cacheKey);
            }

            return result;
        }
    }
}