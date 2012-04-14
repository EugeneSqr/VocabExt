using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

using VX.Domain.Interfaces;
using VX.Domain;

namespace VX.Service.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        public ILanguage GetById(int languageId)
        {
            var result = new Language();
            using (var context = new VXEntities(ConfigurationManager.ConnectionStrings["VXEntities"].ConnectionString))
            {
                result = context.EntityLanguages
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