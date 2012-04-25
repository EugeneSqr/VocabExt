using System.Linq;

using VX.Domain.Interfaces;
using VX.Domain;

namespace VX.Service.Repositories
{
    public class LanguagesRepository : ILanguagesRepository
    {
        private readonly IServiceSettings serviceSettings;
        private readonly IEntitiesFactory entitiesFactory;

        public LanguagesRepository(
            IServiceSettings serviceSettings,
            IEntitiesFactory entitiesFactory)
        {
            this.serviceSettings = serviceSettings;
            this.entitiesFactory = entitiesFactory;
        }

        public ILanguage GetLanguage(int languageId)
        {
            using (var context = new Entities(serviceSettings.ConnectionString))
            {
                var languageEntity = context.Languages.FirstOrDefault(lang => lang.Id == languageId);
                return entitiesFactory.BuildLanguage(languageEntity);
            }
        }
    }
}