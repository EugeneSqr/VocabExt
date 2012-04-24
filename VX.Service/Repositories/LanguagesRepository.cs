using System.Linq;

using VX.Domain.Interfaces;
using VX.Domain;

namespace VX.Service.Repositories
{
    public class LanguagesRepository : ILanguagesRepository
    {
        private readonly IServiceSettings serviceSettings;

        public LanguagesRepository(IServiceSettings serviceSettings)
        {
            this.serviceSettings = serviceSettings;
        }

        public ILanguage GetById(int languageId)
        {
            var result = new Language();
            using (var context = new Entities(serviceSettings.ConnectionString))
            {
                result = context.Languages
                    .Where(lang => lang.Id == languageId)
                    .Select(item => new Language 
                    { 
                        Id = item.Id,
                        Name = item.Name,
                        Abbreviation = item.Abbreviation
                    })
                    .FirstOrDefault();
            }

            return result;
        }
    }
}