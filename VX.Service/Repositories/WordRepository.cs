using System.Linq;
using VX.Domain;
using VX.Domain.Interfaces.Entities;
using VX.Domain.Interfaces.Factories;
using VX.Domain.Interfaces.Repositories;
using VX.Domain.Surrogates;
using VX.Service.Interfaces;

namespace VX.Service.Repositories
{
    public class WordRepository : IWordsRepository
    {
        private readonly IServiceSettings serviceSettings;
        private readonly IEntitiesFactory entitiesFactory;
        
        public WordRepository(
            IServiceSettings serviceSettings,
            IEntitiesFactory entitiesFactory)
        {
            this.serviceSettings = serviceSettings;
            this.entitiesFactory = entitiesFactory;
        }

        public IWord GetById(int wordId)
        {
            var result = new WordSurrogate();
            using (Entities context = new Entities(serviceSettings.ConnectionString))
            {
                var entityWord = context.Words.FirstOrDefault(item => item.Id == wordId);
                if (entityWord != null)
                {
                    result = new WordSurrogate
                                 {
                                     Id = entityWord.Id,
                                     Spelling = entityWord.Spelling,
                                     Transcription = entityWord.Transcription,
                                     Language = entitiesFactory.BuildLanguage(entityWord.Language)
                                 };
                }
            }

            return result;
        }
    }
}