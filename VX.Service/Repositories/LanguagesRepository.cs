using System.Linq;

using VX.Domain;
using VX.Domain.DataContracts.Interfaces;
using VX.Model;
using VX.Service.Factories.Interfaces;
using VX.Service.Infrastructure.Interfaces;
using VX.Service.Repositories.Interfaces;

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