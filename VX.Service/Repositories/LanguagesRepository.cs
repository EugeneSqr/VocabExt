using System.Linq;

using VX.Domain.Interfaces;
using VX.Domain;
using VX.Domain.Surrogates;

namespace VX.Service.Repositories
{
    public class LanguagesRepository : ILanguagesRepository
    {
        private readonly IServiceSettings serviceSettings;

        public LanguagesRepository(IServiceSettings serviceSettings)
        {
            this.serviceSettings = serviceSettings;
        }

        public ILanguage GetLanguage(int languageId)
        {
            ILanguage result;
            using (var context = new Entities(serviceSettings.ConnectionString))
            {
                result = context.Languages
                    .Where(lang => lang.Id == languageId)
                    .Select(lang => new LanguageSurrogate
                                        {
                                            Id = languageId,
                                            Name = lang.Name,
                                            Abbreviation = lang.Abbreviation
                                        })
                    .FirstOrDefault();
            }

            return result;
        }
    }
}