using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VX.Domain.Interfaces
{
    public interface ILanguageRepository
    {
        ILanguage GetById(int languageId);
    }
}
