using Autofac;
using VX.Domain.Interfaces.Factories;
using VX.Domain.Interfaces.Repositories;
using VX.Domain.Surrogates;
using VX.Service.Interfaces;

namespace VX.Service
{
    public class VocabExtService : IVocabExtService
    {
        private readonly ILanguagesRepository languagesRepository;
        private readonly IWordsRepository wordsRepository;
        private readonly IContractItemsFactory contractItemsFactory;

        public VocabExtService()
        {
            languagesRepository = Initializer.Container.Resolve<ILanguagesRepository>();
            contractItemsFactory = Initializer.Container.Resolve<IContractItemsFactory>();
            wordsRepository = Initializer.Container.Resolve<IWordsRepository>();
        }

        public LanguageSurrogate GetLanguage()
        {
            return contractItemsFactory.BuildLanguage(languagesRepository.GetLanguage(1));
        }

        public WordSurrogate GetWord()
        {
            return contractItemsFactory.BuildWord(wordsRepository.GetById(1));
        }
    }
}
