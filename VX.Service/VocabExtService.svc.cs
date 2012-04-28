using Autofac;
using VX.Domain.DataContracts.Interfaces;
using VX.Service.Interfaces;
using VX.Service.Repositories.Interfaces;

namespace VX.Service
{
    public class VocabExtService : IVocabExtService
    {
        private readonly ILanguagesRepository languagesRepository;
        private readonly IWordsRepository wordsRepository;

        public VocabExtService()
        {
            languagesRepository = Initializer.Container.Resolve<ILanguagesRepository>();
            wordsRepository = Initializer.Container.Resolve<IWordsRepository>();
        }

        public ILanguage GetLanguage()
        {
            return languagesRepository.GetLanguage(1);
        }

        public IWord GetWord()
        {
            return wordsRepository.GetById(1);
        }
    }
}
