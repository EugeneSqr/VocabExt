using System.Linq;
using VX.Domain;
using VX.Domain.Interfaces;

namespace VX.Service.Repositories
{
    public class WordRepository : IWordsRepository
    {
        private readonly ILanguagesRepository languagesRepository;
        
        public WordRepository(ILanguagesRepository languagesRepository)
        {
            this.languagesRepository = languagesRepository;
        }

        public IWord GetById(int wordId)
        {
            var result = new Word();
            using (Entities context = new Entities())
            {
                var entityWord = context.Words.FirstOrDefault(item => item.Id == wordId);
                if (entityWord != null)
                {
                    result = new Word
                                 {
                                     Id = entityWord.Id,
                                     Spelling = entityWord.Spelling,
                                     Transcription = entityWord.Transcription,
                                     Language = languagesRepository.GetLanguage(entityWord.LanguageId)
                                 };
                }
            }

            return result;
        }
    }
}