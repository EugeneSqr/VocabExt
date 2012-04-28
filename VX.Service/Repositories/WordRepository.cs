using System.Linq;
using VX.Domain;
using VX.Domain.DataContracts;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Factories.Interfaces;
using VX.Service.Infrastructure.Interfaces;
using VX.Service.Repositories.Interfaces;

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
            var result = new WordContract();
            using (Entities context = new Entities(serviceSettings.ConnectionString))
            {
                var entityWord = context.Words.FirstOrDefault(item => item.Id == wordId);
                if (entityWord != null)
                {
                    result = new WordContract
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